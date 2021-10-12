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
        public void Post([FromBody] string jsonInput)
        {
            // Get schema and use for validating
            if (new JsonValidator<Article[]>("").IsValid(jsonInput, out Article[] articles))
            {
                // Store in DB
            }
        }
        
        [HttpPost]
        [Route("/[controller]/PostJsonSchema")]
        public void PostJsonSchema([FromBody] JsonElement jsonInput)
        {
            WordCountDbContext dbContext = new();
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            string jsonString = jsonInput.GetRawText();
            JsonSchemaDataModel schemaData = JsonSerializer.Deserialize<JsonSchemaDataModel>(jsonString, options);

            if (schemaData == null)
            {
                Console.WriteLine("json deserialization failed and returned null.");
                return;
            }

            JsonSchemaModel model = new()
            {
                SchemaName = schemaData.SchemaName,
                JsonString = schemaData.SchemaBody.GetRawText()
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