using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordCount.Data;
using WordCount.Data.DataAccess;
using WordCount.Data.Models;

namespace WordCount.Controllers
{
    public sealed class WordRatioController : Controller
    {
        private IQueryable<WordRatio> dbSet;
        // private IUnitOfWork unitOfWork;

        public WordRatioController()
        {
            // unitOfWork = new UnitOfWork(new ArticleContext());
            dbSet = new ArticleContext().WordRatios.AsQueryable();
        }

        [HttpGet]
        [Route("/[controller]/")]
        public IActionResult GetMatches(string[] terms, string[] sources)
        {
            if (terms.Length == 0)
            {
                return BadRequest("No terms given.");
            }

            // IEnumerable<WordRatio> result = unitOfWork.WordRatioRepository.FindAll(w => terms.Contains(w.Word));
            var result = from ratio in dbSet
                where terms.Contains(ratio.Word)
                select ratio;

            if (sources.Length != 0)
            {
                result = from ratio in result
                    where sources.Contains(ratio.PublisherName)
                    select ratio;
            }

            result = result.OrderBy(a => a.Title);
            return Ok(result);
        }
    }
}