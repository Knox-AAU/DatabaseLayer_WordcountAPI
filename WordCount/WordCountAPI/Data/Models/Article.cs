using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WordCount.Controllers.JsonInputModels;

namespace WordCount.Data.Models
{
    [Table("Article")]
    public sealed class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
        public int TotalWords { get; set; }

        public Publisher Publisher { get; set; }
        public List<HasWord> ContainedWords { get; set; }

        public static Article CreateFromJsonModel(ArticleJsonModel jsonModel)
        {
            List<HasWord> terms = new(jsonModel.Words.Length);

            foreach (TermJsonModel term in jsonModel.Words)
            {
                terms.Add(new HasWord { Count = term.Amount, Word = new Word(term.Word) });
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
                ContainedWords = terms
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
}