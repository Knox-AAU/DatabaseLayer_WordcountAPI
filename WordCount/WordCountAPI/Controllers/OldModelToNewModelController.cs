using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordCount.Data;
using WordCount.Data.Models;

namespace WordCount.Controllers
{
    public sealed class OldModelToNewModelController : ControllerBase
    {
        public OldModelToNewModelController()
        {
            this.OldContext = new ArticleContextOld();
            this.NewContext = new ArticleContext();
        }

        public ArticleContext NewContext { get; set; }

        public ArticleContextOld OldContext { get; set; }

        [HttpGet]
        [Route("/[controller]")]
        public IActionResult Migrate(string pw)
        {
            if (pw != "please_do_migration")
                return BadRequest();

            var oldArticles = OldContext.Articles;
            DbSet<Publisher_old> oldPublishers = OldContext.Publishers;

            var newArticles = NewContext.Articles;
            var newPublishers = NewContext.Publishers;

            oldArticles.AsQueryable();

            foreach (var publisher in oldPublishers)
            {
                // The publisher is a root of the data base structure. It is not possible to have duplicated,
                // thus no need to check first.
                Publisher createdPublisher = new() { PublisherName = publisher.PublisherName };
                IEnumerable<Article_old> firstArticles = publisher.Articles.Take(50);
                
                // build entire structure, then add to root structure.
                AddArticles(firstArticles, createdPublisher);
                newPublishers.Add(createdPublisher);
                NewContext.SaveChanges();
            }

            return null;
        }

        private void AddArticles(IEnumerable<Article_old> firstArticles, Publisher publisher)
        {
            foreach (Article_old oldArticle in firstArticles)
            {
                // Publishers have guaranteed unique articles, therefore no need to check if articles exist
                // in new database.
                var createdArticle = new Article()
                {
                    Id = oldArticle.Id, TotalWords = oldArticle.TotalWords,
                    FilePath = oldArticle.FilePath, Title = oldArticle.Title, Publisher = publisher
                };

                AddWordRatio(createdArticle, oldArticle.Terms);
            }
        }

        private void AddWordRatio(Article createdArticle, List<Term> oldArticleTerms)
        {
            foreach (Term term in oldArticleTerms)
            {
                // We risk duplicate words in database, therefore check if it is already added.
                // If it is added, then use that reference!
                Word wordToAdd = NewContext.Words.Find(term.Word) ?? new Word { Literal = term.Word };
                createdArticle.WordOccurrences.Add(new WordOccurrence
                {
                    ArticleId = createdArticle.Id,
                    Article = createdArticle,
                    Count = term.Count,
                    Word = wordToAdd
                });
            }
        }
    }
}