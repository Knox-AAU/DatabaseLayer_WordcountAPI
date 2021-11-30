using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WordCount.Controllers.JsonInputModels;
using WordCount.Data.DataAccess;

namespace WordCount.Data.Models
{
    [Table("Article")]
    public sealed class Article_OLD : DatabaseEntityModel<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
        public int TotalWords { get; set; }

        public Publisher Publisher { get; set; }
        public List<WordOccurance> Words { get; set; }

        [NotMapped]
        public override long PrimaryKey => Id;

        public static Article CreateFromJsonModel(ArticleJsonModel jsonModel)
        {
            List<WordOccurance> terms = new(jsonModel.Words.Length);

            foreach (TermJsonModel term in jsonModel.Words)
            {
                terms.Add(new WordOccurance() { Count = term.Amount, Word = new Term() { Literal = term.Word}});
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
                Words = terms
            };
        }

        public static IEnumerable<Article> CreateFromJsonModels(IEnumerable<ArticleJsonModel> jsonModels)
        {
            IEnumerable<ArticleJsonModel> articleJsonModels = jsonModels as ArticleJsonModel[] ?? jsonModels.ToArray();
            List<Article> articles = new(articleJsonModels.Count());

            foreach (ArticleJsonModel jsonModel in articleJsonModels)
            {
                articles.Add(CreateFromJsonModel(jsonModel));
            }

            return articles;
        }
    }

    public class WordOccurance : DatabaseEntityModel<long>
    {
        public long Id { get; set; }
        public Article Article { get; set; }
        public int Count { get; set; }
        public Term Word { get; set; }


        public override long PrimaryKey => Id;
    }
}