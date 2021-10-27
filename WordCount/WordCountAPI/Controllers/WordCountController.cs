using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WordCount.Data;
using WordCount.JsonModels;
using WordCount.Models;
using Article = WordCount.Models.Article;

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
            ArticleContext context = new ArticleContext();
            JsonSchemaModel? schema = context.JsonSchemas.ToList().Find(s => s.SchemaName == WordCountSchemaName);
            string jsonInput = jsonElement.GetRawText();
            
            // Get schema and use for validating
            if (schema == null || !new JsonValidator<ArticleJsonModel[]>(schema.JsonString).IsValid(jsonInput, out ArticleJsonModel[] jsonArticles))
            {
                return BadRequest("Wrong body syntax, does not follow schema.");
            }

            //Create article
            IEnumerable<Article> articles = Article.CreateFromJsonModels(jsonArticles);
            context.Articles.AddRange(articles);
            context.SaveChanges();
            
            return Ok();
        }
        
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new List<string>();
        }

        [HttpGet]
        [Route("/[controller]/filelist/{id:int}")]
        public IActionResult GetFilepath(long id)
        {
            try
            {
                //var x = new WordCountDbContext().Articles.First(e => e.Id == id).FilePath;
                return null;
                
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
            
            return Ok();
        }
    }
}