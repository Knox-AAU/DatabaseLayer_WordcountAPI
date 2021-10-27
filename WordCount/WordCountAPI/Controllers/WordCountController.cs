using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WordCount.Controllers.JsonInputModels;
using WordCount.Data;
using WordCount.Data.Models;
using WordCount.JsonModels;
using WordCount.Models;
using Article = WordCount.Data.Models.Article;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : ControllerBase
    {
        private const string WordCountSchemaName = "wordcount";
        private readonly ArticleContext context;

        public WordCountController()
        {
            context = new ArticleContext();
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] JsonElement jsonElement)
        {
            JsonSchemaModel? schema = context.JsonSchemas.ToList().Find(s => s.SchemaName == WordCountSchemaName);
            string jsonInput = jsonElement.GetRawText();

            if (schema == null)
            {
                return StatusCode(500, $"\"{WordCountSchemaName}\" schema does not exist.");
            }
            
            // Get schema and use for validating
            if (!new JsonValidator<ArticleJsonModel[]>(schema.JsonString).IsValid(jsonInput, out ArticleJsonModel[] jsonArticles))
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
                string filePath = context.Articles.First(e => e.Id == id).FilePath;
                return new JsonResult(new FileIdResponse(filePath));
            }
            catch (Exception)
            {
                return BadRequest($"No entity with ID {id} exists");
            }
        }

    }
}