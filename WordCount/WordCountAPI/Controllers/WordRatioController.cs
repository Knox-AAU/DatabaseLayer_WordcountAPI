using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WordCount.Data;
using WordCount.DataAccess;

namespace WordCount.Controllers
{
    public partial class WordCountController
    {
        [HttpGet]
        [Route("/[controller]/getAllWordRatios")]
        public IEnumerable<WordRatios> GetAllWordRatios()
        {
            return new WordCountDbContext().WordRatios.ToList();
        }
    }
}