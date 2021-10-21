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
        
        [HttpGet]
        [Route("/[controller]/")]
        public IEnumerable<WordRatios> GetMatches(string[] terms, string[] sources)
        { 
            if (terms == null || terms.Length == 0)
            {
                //TODO Error message    
                return null;
            }
            
            List<WordRatios> set = new WordCountDbContext().WordRatios.ToList();
            IEnumerable<WordRatios> result = set.Where(w => terms.Contains(w.WordName));
            
            if (sources != null && sources.Length != 0)
                result = result.Where(r => sources.Contains(r.SourceName));


            return result.OrderBy(a => a.ArticleTitle);
        }
    }
}