using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_first_test.Models
{
    public sealed class Article
    {
        [Key]
        public long ArticleId { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public int TotalWordsInArticle { get; set; }
        public ICollection<OccursIn> ContainedWords { get; set; }
        
        [ForeignKey("Name")]
        public Publisher Publisher { get; set; }
    }
}