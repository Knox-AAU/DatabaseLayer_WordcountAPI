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
using Article = WordCount.Data.Models.Article;

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

            //Insert article
            unitOfWork.ArticleRepository.Insert(result);
            return Ok(message.ToString());
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

        private IEnumerable<Article> RemoveDuplicates(IEnumerable<ArticleJsonModel> jsonArticles, out StringBuilder responseMessage)
        {
            IEnumerable<ArticleJsonModel> articleJsonModels = jsonArticles as ArticleJsonModel[] ?? jsonArticles.ToArray();
            List<Article> result = new(articleJsonModels.Count());
            responseMessage = new StringBuilder();
            
            foreach (var articleJsonModel in articleJsonModels)
            {
                Article article = Article.CreateFromJsonModel(articleJsonModel);
                if (unitOfWork.ArticleRepository.TryGetEntity(article, out Article existingArticle))
                {
                    responseMessage.Append($"{article.Title} is already in database.\n");
                    continue;
                }
                
                if (unitOfWork.PublisherRepository.TryGetEntity(article.Publisher, out Publisher existingPublisher))
                {
                    article.Publisher = existingPublisher;
                }
                result.Add(article);
            }

            return result;
        }
    }
}