using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordCount.Controllers.JsonInputModels;
using WordCount.Controllers.ResponseModels;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : ControllerBase
    {
        private const string WordCountSchemaName = "wordcount";

        private readonly wordcountContext databaseContext = new();

        [HttpPost]
        public IActionResult Post([FromBody] JsonElement jsonElement)
        {
            JsonSchema? schema = databaseContext.JsonSchemas.First(s => s.SchemaName == WordCountSchemaName);
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

            databaseContext.Articles.AddRange(enumerable);

            foreach (Article article in enumerable)
            {
                Console.WriteLine($"Added {article.Title}");
            }

            databaseContext.SaveChanges();
            return Ok(message.ToString());
        }

        [HttpGet]
        [Route("/[controller]/filelist/{id:int}")]
        public IActionResult GetFilepath(long id)
        {
            try
            {
                string filePath = databaseContext.Articles.First(e => e.Id == id).FilePath;
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
                int fileCount = databaseContext.Articles.Count();
                return new JsonResult(fileCount);
            }
            catch (Exception e)
            {
                return BadRequest($"An error occured: {e.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Status()
        {
            if (await databaseContext.Database.CanConnectAsync())
            {
                return Ok();
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, "Connection to database could not be established.");
        }

        private IEnumerable<Article> RemoveDuplicates(IEnumerable<ArticleJsonModel> jsonArticles, out StringBuilder responseMessage)
        {
            responseMessage = new StringBuilder();
            IEnumerable<ArticleJsonModel> articleJsonModels = jsonArticles as ArticleJsonModel[] ?? jsonArticles.ToArray();

            // Check for existing publisher only once - each post request
            // contain only articles from same publisher.
            Publisher publisher = databaseContext.Publishers.FirstOrDefault(p => p.PublisherName == articleJsonModels.First().Publication);

            if (publisher == null)
            {
                publisher = new Publisher { PublisherName = articleJsonModels.First().Publication };
                databaseContext.Publishers.Add(publisher);
            }

            List<Article> result = new(articleJsonModels.Count());

            foreach (ArticleJsonModel articleJsonModel in articleJsonModels)
            {
                Article article = Article.CreateFromJsonModel(articleJsonModel);
                article.Id = databaseContext.Articles.OrderBy(x => x.Id).Last().Id + 1;

                if (databaseContext.Articles.FirstOrDefault(a => a.Title == articleJsonModel.ArticleTitle) != null)
                {
                    responseMessage.Append($"{article.Title} is already in database.\n");
                    continue;
                }

                List<Word> words = new List<Word>(articleJsonModel.TotalWordsInArticle);
                List<OccursIn> occursIns = new List<OccursIn>();
                foreach (var word in articleJsonModel.Words)
                {
                    var x = databaseContext.Words.FirstOrDefault(w => word.Word == w.Text);
                    if (x == null)
                    {
                        x = new Word(word.Word);
                        words.Add(x);
                    }

                    occursIns.Add(new OccursIn()
                    {
                        Article = article,
                        Word = x.Text,
                        Count = word.Amount
                    });
                }

                databaseContext.Words.AddRange(words);

                article.OccursIns = occursIns;
                article.PublisherName = publisher.PublisherName;

                result.Add(article);
            }

            return result;
        }
    }
}