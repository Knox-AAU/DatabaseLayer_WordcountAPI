using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordCount.Controllers.JsonInputModels;
using WordCount.DataAccess;
using WordCount.JsonModels;
using WordCount.Models;

namespace WordCount.Data.Models
{
    [Table("Article")]
    public sealed class Article : DatabaseEntityModel<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
        public int TotalWords { get; set; }
        public Publisher Publisher { get; set; }
        public List<Term> Terms { get; set; }

        [NotMapped]
        public override long PrimaryKey => Id;

        public static Article CreateFromJsonModel(ArticleJsonModel jsonModel)
        {
            List<Term> terms = new(jsonModel.Words.Length);
            
            foreach (TermJsonModel term in jsonModel.Words)
            {
                terms.Add(new Term { Count = term.Amount, Word = term.Word });
            }

            return new Article
            {
                FilePath = jsonModel.FilePath,
                Title = jsonModel.ArticleTitle,
                TotalWords = jsonModel.TotalWordsInArticle,
                Publisher = new Publisher
                {
                    PublisherName = jsonModel.Publication
                },
                Terms = terms
            };
        }

        public static IEnumerable<Article> CreateFromJsonModels(ICollection<ArticleJsonModel> jsonModels)
        {
            List<Article> articles = new(jsonModels.Count);

            foreach (ArticleJsonModel jsonModel in jsonModels)
            {
                articles.Add(CreateFromJsonModel(jsonModel));
            }

            return articles;
        }
    }
}