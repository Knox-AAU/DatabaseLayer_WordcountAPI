using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WordCount.Controllers.JsonInputModels;
using WordCount.Data;
using WordCount.Data.Models;
using WordCount.DataAccess;
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
        private UnitOfWork unitOfWork;

        public WordCountController()
        {
            unitOfWork = new UnitOfWork(new ArticleContext());
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] JsonElement jsonElement)
        {
            JsonSchemaModel? schema = unitOfWork.SchemaRepository.Find(s => s.SchemaName == WordCountSchemaName);
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

            string responseMessage = string.Empty;
            IEnumerable<Article> result = RemoveDuplicates(jsonArticles);

            //Create article
            unitOfWork.ArticleRepository.Insert(result);
            return Ok(responseMessage);
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
                string filePath = unitOfWork.ArticleRepository.Find(e => e.Id == id).FilePath;
                return new JsonResult(new FileIdResponse(filePath));
            }
            catch (Exception)
            {
                return BadRequest($"No entity with ID {id} exists");
            }
        }

        private IEnumerable<Article> RemoveDuplicates(IEnumerable<ArticleJsonModel> jsonArticles)
        {
            List<Article> result = new();

            foreach (var articleJsonModel in jsonArticles)
            {
                Article article = Article.CreateFromJsonModel(articleJsonModel);
                Publisher existingPublisher = unitOfWork.publisherRepository.Find(p => p.PublisherName == article.Publisher.PublisherName);

                if (existingPublisher != null)
                {
                    article.Publisher = existingPublisher;
                }

                result.Add(article);
            }

            return result;
        }
    }
}