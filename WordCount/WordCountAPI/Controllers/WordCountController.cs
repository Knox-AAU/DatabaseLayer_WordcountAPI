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
        private readonly GetController getController = new GetController();

        [HttpPost]
        public void Post([FromBody] string jsonInput)
        {
            // Get schema and use for validating
            if (new JsonValidator<Article[]>("").IsValid(jsonInput, out Article[] articles))
            {
                // Store in DB
            }
        }
        
        [HttpPost]
        [Route("/[controller]/PostJsonSchema/{schemaName}")]
        public void PostJsonSchema([FromBody] JsonElement jsonInput, string schemaName)
        {
            var dbContext = new WordCountDbContext();

            var model = new JsonSchemaModel
            {
                SchemaName = schemaName,
                JsonString = jsonInput.ToString()
            };

            dbContext.JsonSchemas.Add(model);
            dbContext.SaveChanges();
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