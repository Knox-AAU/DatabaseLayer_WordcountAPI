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
        [HttpPost]
        public void Post([FromBody] JsonElement jsonElement)
        {
            WordCountDbContext dbContext = new();
            var schema = dbContext.JsonSchemas.ToList().Find(s => s.SchemaName == "wordcount");

            string jsonInput = jsonElement.GetRawText();

            // Get schema and use for validating
            if (new JsonValidator<Article[]>(schema.JsonString).IsValid(jsonInput, out Article[] articles))
            {
                foreach (Article article in articles)
                {
                    FileListModel fileListModel = new()
                    {
                        ArticleTitle = article.ArticleTitle,
                        FilePath = article.FilePath,
                        TotalWordsInArticle = article.TotalWordsInArticle
                    };
                    
                    List<WordListModel> words = new();
                    List<AppearsInModel> appearsInModels = new();

                    foreach (WordData articleWord in article.Words)
                    {
                        WordListModel wordListModel = new()
                        {
                            WordName = articleWord.Word,
                        };

                        AppearsInModel appearsInModel = new()
                        {
                            Amount = articleWord.Amount,
                            WordName = articleWord.Word,
                            ArticleTitle = article.ArticleTitle,
                            FilePath = article.FilePath
                        };
                        
                        words.Add(wordListModel);
                        appearsInModels.Add(appearsInModel);
                    }

                    dbContext.FileList.Add(fileListModel);
                    dbContext.Wordlist.AddRange(words);
                    dbContext.AppearsIn.AddRange(appearsInModels);
                }

                dbContext.SaveChanges();
                
                Response.StatusCode = 200;
                Console.WriteLine($"Added {articles.Length} entries.");
            }
            else
            {
                Response.StatusCode = 400;
            }

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