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
            this.OldContext = new OldContext();
            this.Context = new ArticleContext();
        }

        public ArticleContext Context { get; set; }

        public OldContext OldContext { get; set; }

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

            var newArticles = Context.Articles;
            var newPublishers = Context.Publishers;
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
                    Context.SaveChanges();
                    addedArticles += 50;
                }
                while (articlesToAggregate.Any()); // if any articles are left
            }

            return Ok();
        }

        private void AggregateArticles(IEnumerable<Article_old> firstArticles,  Publisher newPublisher)
        {
            foreach (Article_old oldArticle in firstArticles)
            {
                var createdArticle = new Article()
                {
                    Id = oldArticle.Id, // Guaranteed unique articles for each publisher, therefore no need to check if articles exist in DB
                    TotalWords = oldArticle.TotalWords,
                    FilePath = oldArticle.FilePath,
                    Title = oldArticle.Title,
                    Publisher = newPublisher // The publisher is a root of the data base structure. Thus, no duplicates in database. No check needed.
                };

                AggregateWordOccurence(createdArticle, oldArticle.Terms);
                newPublisher.Articles.Add(createdArticle);
            }
        }

        private void AggregateWordOccurence(Article createdArticle, List<Term_old> oldArticleTerms)
        {
            foreach (Term_old term in oldArticleTerms)
            {
                // We risk duplicate words in database, therefore check if it is already added.
                // If it is added, then use that reference!
                Word wordToAdd = Context.Words.First(a => a.Literal == term.Word) ?? new Word { Literal = term.Word };
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