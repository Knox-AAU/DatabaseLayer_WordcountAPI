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
                return new ObjectResult("Wrong body syntax, does not follow schema.") { StatusCode = 400 };
            }

            List<AppearsInModel> appearsInModels = new();
                
            foreach (Article article in articles)
            {
                FileListModel fileListModel = JsonDbUtility.ArticleToFileList(article);

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

            return new ObjectResult(message == string.Empty ? "Ok" : message) { StatusCode = statusCode };
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
        public WordListModel Get(int id)
        {
            WordListModel entity = new WordCountDbContext().Wordlist.Find(id);

            return entity;
        }
        
        [HttpGet]
        [Route("/[controller]/WordRatios")]
        public string GetWordRatios()
        {
            var dbContext = new WordCountDbContext();

            return string.Empty;
        }
    }
}