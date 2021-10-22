using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_first_test.Models
{
    public sealed class Publisher
    {
        [Key] public long PublisherId { get; set; }
        public string Name { get; set; }
        
        [ForeignKey("ArticleId")]
        public ICollection<Article> Articles { get; set; }
    }
}