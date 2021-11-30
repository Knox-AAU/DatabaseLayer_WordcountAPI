using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
            {
                return BadRequest();
            }

            var oldArticles = OldContext.Articles;
            DbSet<Publisher_old> oldPublishers = OldContext.Publishers;

            var newArticles = NewContext.Articles;
            var newPublishers = NewContext.Publishers;
            oldArticles.AsQueryable();

            foreach (var oldPublisher in oldPublishers)
            {
                int addedArticles = 0;
                IEnumerable<Article_old> articlesToAggregate;
                do
                {
                    // Create root publisher
                    Publisher newPublisher = new Publisher() { PublisherName = oldPublisher.PublisherName };
                    articlesToAggregate = oldPublisher.Articles.Skip(addedArticles).Take(50);

                    // Create and attach/aggregate articles to root.
                    AggregateArticles(articlesToAggregate, newPublisher);

                    // Save the first 50
                    NewContext.SaveChanges();
                    addedArticles += 50;
                }
                while (articlesToAggregate.Any()); // if any articles are left
            }

            return null;
        }

        private void AggregateArticles(IEnumerable<Article_old> firstArticles,  Publisher newPublisher)
        {
            foreach (Article_old oldArticle in firstArticles)
            {
                // The publisher is a root of the data base structure.
                // It is not possible to have duplicates of it, thus no need to check in DB first.
                // Publishers have guaranteed unique articles, therefore no need to check if articles exist
                // in new database.
                var createdArticle = new Article()
                {
                    Id = oldArticle.Id,
                    TotalWords = oldArticle.TotalWords,
                    FilePath = oldArticle.FilePath,
                    Title = oldArticle.Title,
                    Publisher = newPublisher
                };

                AggregateWordOccurance(createdArticle, oldArticle.Terms);
            }
        }

        private void AggregateWordOccurance(Article createdArticle, List<Term> oldArticleTerms)
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