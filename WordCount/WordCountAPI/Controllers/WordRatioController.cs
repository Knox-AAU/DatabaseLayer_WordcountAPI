using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WordCount.Data;
using WordCount.Data.DataAccess;
using WordCount.Data.Models;

namespace WordCount.Controllers
{
    public sealed class WordRatioController : Controller
    {
        private IUnitOfWork unitOfWork;

        public WordRatioController()
        {
            unitOfWork = new UnitOfWork(new ArticleContext());
        }
        
        [HttpGet]
        [Route("/[controller]/all")]
        public IEnumerable<WordRatio> GetAllWordRatios()
        {
            return unitOfWork.WordRatioRepository.All();
        }
        
        [HttpGet]
        [Route("/[controller]/")]
        public IActionResult GetMatches(string[] terms, string[] sources)
        {
            if (terms.Length == 0)
            {
                return BadRequest("No terms given.");
            }
            IEnumerable<WordRatio> result = unitOfWork.WordRatioRepository.FindAll(w => terms.Contains(w.Word));
            if (sources.Length != 0)
            {
                result = result.Where(r => sources.Contains(r.PublisherName));
            }

            result = result.OrderBy(a => a.Title);
            return Ok(result);
        }
    }
}