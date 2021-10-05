using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WordCount.Data;
using WordCount.Models;

namespace WordCount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCountController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> words = new();
            new WordCountContext().wordlist.Take(100).ToList().ForEach(wordList => words.Add(wordList.WordName));
            return words;
        }
    }
}