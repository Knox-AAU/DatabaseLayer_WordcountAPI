using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    [Table("Term")]
    public sealed class Term
    {
        public long ArticleId { get; set; }
        public string Word { get; set; }
        public int Count { get; set; }
    }
}