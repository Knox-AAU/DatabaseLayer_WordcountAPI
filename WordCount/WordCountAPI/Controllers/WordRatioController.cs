using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WordCount.Data;
using WordCount.DataAccess;

namespace WordCount.Controllers
{
    public sealed class WordRatioController : Controller
    {
        private UnitOfWork unitOfWork;

        public WordRatioController()
        {
            unitOfWork = new UnitOfWork(new ArticleContext());
        }
        
        [HttpGet]
        [Route("/[controller]/all")]
        public IEnumerable<WordRatio> GetAllWordRatios()
        {
            return null;
        }
        
        [HttpGet]
        [Route("/[controller]/")]
        public IActionResult GetMatches(string[] terms, string[] sources)
        {
            if (terms.Length == 0)
            {
                return BadRequest("No terms given.");
            }


            List<WordRatio> set = unitOfWork.WordRatioRepository.All().ToList();
            IEnumerable<WordRatio> result = set.Where(w => terms.Contains(w.Word));
            
            
            if (sources.Length != 0)
            {
                result = result.Where(r => sources.Contains(r.PublisherName));
            }

            result = result.OrderBy(a => a.Title);
            return Ok(result);
        }
    }
}