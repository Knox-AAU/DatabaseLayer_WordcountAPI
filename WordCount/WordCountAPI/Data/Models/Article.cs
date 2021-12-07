using System;
using System.Collections.Generic;
using WordCount.Controllers.JsonInputModels;

#nullable disable

namespace WordCount
{
    public partial class Article
    {
        public Article()
        {
            OccursIns = new HashSet<OccursIn>();
        }

        public long Id { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
        public int? TotalWords { get; set; }
        public string PublisherName { get; set; }

        public virtual Publisher PublisherNameNavigation { get; set; }
        public virtual ICollection<OccursIn> OccursIns { get; set; }

        public static Article CreateFromJsonModel(ArticleJsonModel jsonModel)
        {
            List<OccursIn> terms = new(jsonModel.Words.Length);

            foreach (TermJsonModel term in jsonModel.Words)
            {
                terms.Add(new OccursIn { Count = term.Amount, Word = term.Word });
            }

            return new Article
            {
                FilePath = jsonModel.FilePath,
                Title = jsonModel.ArticleTitle,
                TotalWords = jsonModel.TotalWordsInArticle,
                PublisherName = jsonModel.Publication,
                OccursIns = terms
            };
        }
    }
}
