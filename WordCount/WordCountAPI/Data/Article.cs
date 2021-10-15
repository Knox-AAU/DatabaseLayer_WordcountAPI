using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code_first_test.Models
{
    public class Article
    {
        [Key]
        public long ArticleId { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public int TotalWordsInArticle { get; set; }
        public ICollection<WordOccurances> Words { get; set; }
        public Publisher Publisher { get; set; }
    }
}