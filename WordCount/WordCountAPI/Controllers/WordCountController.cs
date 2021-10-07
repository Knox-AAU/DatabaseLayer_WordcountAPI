using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WordCount.Data;
using WordCount.JsonModels;
using WordCount.Models;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : ControllerBase
    {
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
        public void Get([FromBody] int id)
        {
        }

        [HttpPost]
        public void Post([FromBody] string jsonInput)
        {
            // Get schema and use for validating
            if (new JsonValidator<Article[]>("").IsValid(jsonInput, out Article[] articles))
            {
                // Store in DB
            }
        }
    }
}