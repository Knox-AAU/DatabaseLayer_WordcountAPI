using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WordCount.Data;
using WordCount.DataAccess;

namespace WordCount.Controllers
{
    public sealed class WordRatioController : Controller
    {
        [HttpGet]
        [Route("/[controller]/all")]
        public IEnumerable<WordRatios> GetAllWordRatios()
        {
            return new WordCountDbContext().WordRatios.ToList();
        }
    }
}