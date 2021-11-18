using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WordCount.Controllers.JsonInputModels;
using WordCount.Controllers.ResponseModels;
using WordCount.Data;
using WordCount.Data.DataAccess;
using WordCount.Data.Models;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : ControllerBase
    {
        private const string WordCountSchemaName = "wordcount";
        private IUnitOfWork unitOfWork;

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

            IEnumerable<Article> result = RemoveDuplicates(jsonArticles, out StringBuilder message);

            // Insert article
            IEnumerable<Article> enumerable = result as Article[] ?? result.ToArray();

            unitOfWork.ArticleRepository.Insert(enumerable);
            foreach (var article in enumerable)
            {
                Console.WriteLine("ADDED " + article.Title);
            }

            return Ok(message.ToString());
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

        [HttpGet]
        [Route("/[controller]/fileCount")]
        public IActionResult GetFileCount()
        {
            try
            {
                int fileCount = unitOfWork.ArticleRepository.All().Count;
                return new JsonResult(fileCount);
            }
            catch (Exception e)
            {
                return BadRequest($"An error occured: {e.Message}");
            }
        }
        
        private IEnumerable<Article> RemoveDuplicates(IEnumerable<ArticleJsonModel> jsonArticles, out StringBuilder responseMessage)
        {
            responseMessage = new StringBuilder();
            IEnumerable<ArticleJsonModel> articleJsonModels = jsonArticles as ArticleJsonModel[] ?? jsonArticles.ToArray();

            // Check for existing publisher only once - each post request
            // contain only articles from same publisher.
            Publisher publisher = unitOfWork.PublisherRepository.Find(p => p.PublisherName == articleJsonModels.First().Publication);
            if (publisher == null)
            {
                publisher = new Publisher { PublisherName = articleJsonModels.First().Publication };
            }

            List<Article> result = new(articleJsonModels.Count());
            foreach (var articleJsonModel in articleJsonModels)
            {
                Article article = Article.CreateFromJsonModel(articleJsonModel);
                article.Publisher = publisher;
                if (unitOfWork.ArticleRepository.Find(a => a.Title == articleJsonModel.ArticleTitle) != null)
                {
                    responseMessage.Append($"{article.Title} is already in database.\n");
                    continue;
                }

                result.Add(article);
            }

            return result;
        }
    }
}