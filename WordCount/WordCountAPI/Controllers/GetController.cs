using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WordCount.Data;
using WordCount.Models;

namespace WordCount.Controllers
{
    public sealed class GetController : ControllerBase
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
        }
    }
}
