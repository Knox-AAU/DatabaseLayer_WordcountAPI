using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordCount.JsonModels;

namespace WordCount.Models
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
        public List<Term> Terms { get; set; }

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
    }
}