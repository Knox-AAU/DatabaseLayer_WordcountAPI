using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WordCount.Data;
using WordCount.JsonModels;
using WordCount.Models;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : ControllerBase
    {
        private const string WordCountSchemaName = "wordcount";
        
        [HttpPost]
        public IActionResult Post([FromBody] JsonElement jsonElement)
        {
            WordCountDbContext dbContext = new();
            JsonSchemaModel? schema = dbContext.JsonSchemas.ToList().Find(s => s.SchemaName == WordCountSchemaName);

            string jsonInput = jsonElement.GetRawText();
            string message = string.Empty;

            int statusCode = 200;

            // Get schema and use for validating
            if (!new JsonValidator<Article[]>(schema.JsonString).IsValid(jsonInput, out Article[] articles))
            {
                return BadRequest("Wrong body syntax, does not follow schema.");
            }

            List<AppearsInModel> appearsInModels = new();
                
            foreach (Article article in articles)
            {
                FileListModel fileListModel = JsonDbUtility.ArticleToFileList(article);

                if (!dbContext.ExternalSources.ToList().Exists(e => e.SourceName == article.Publication))
                {
                    dbContext.ExternalSources.Add(new ExternalSourcesModel { SourceName = article.Publication });
                    dbContext.SaveChanges();
                }

                if (dbContext.FileList.ToList().Exists(a => a.ArticleTitle == fileListModel.ArticleTitle))
                {
                    statusCode = 206;
                    message += $"Article with title \"{fileListModel.ArticleTitle}\" already exists in the database.\n";
                }

                if (dbContext.FileList.ToList().Exists(a => a.FilePath == fileListModel.FilePath))
                {
                    statusCode = 206;
                    message += $"Article with file path \"{fileListModel.FilePath}\" already exists in the database.\n";
                }
                    
                if (statusCode == 206) continue;
                    
                List<WordListModel> words = new();
                appearsInModels = new();

                foreach (WordData articleWord in article.Words)
                {
                    WordListModel wordListModel = JsonDbUtility.WordDataToWordList(articleWord);
                    AppearsInModel appearsInModel = JsonDbUtility.ArticleWordDataToAppearsIn(article, articleWord);

                    if (dbContext.Wordlist.Find(wordListModel.WordName) == null)
                    {
                        words.Add(wordListModel);
                    }

                    appearsInModels.Add(appearsInModel);
                }

                dbContext.Wordlist.AddRange(words);
                dbContext.FileList.Add(fileListModel);
            }
                
            dbContext.SaveChanges();
            dbContext.AppearsIn.AddRange(appearsInModels);
            dbContext.SaveChanges();

            Console.WriteLine($"Added {articles.Length} entries.");

            return Ok(message == string.Empty ? "Ok" : message);
        }
        
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            // Get all words
            List<string> words = new();
            new WordCountDbContext().Wordlist.Take(100).ToList().ForEach(wordList => words.Add(wordList.WordName));
            return words;
        }

        [HttpGet]
        [Route("/[controller]/{id:int}")]
        public IActionResult GetFilepath(long id)
        {
            try
            {
                var x = new WordCountDbContext().FileList.First(e => e.Id == id).FilePath;
                return new JsonResult(new FileIdResponse(x));
            }
            catch (Exception e)
            {
                return BadRequest("No such entity");
            }
        }
    
        
        [HttpGet]
        [Route("/[controller]/{word}")]
        public IActionResult Get(string word)
        {
            WordListModel entity = new WordCountDbContext().Wordlist.Find(word);

            if (entity == null)
            {
                return NotFound($"Word \"{word}\" does not exist in the database.");
            }

            return Ok(entity);
        }
    }

    public class FileIdResponse
    {
        public string FilePath { get; set; }
        public FileIdResponse(string s)
        {
            FilePath = s;
        }
    }
}