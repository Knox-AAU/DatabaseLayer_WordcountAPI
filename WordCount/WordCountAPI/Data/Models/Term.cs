using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Data.Models
{
    public sealed class HasWord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public long Id { get; set; }

        [ForeignKey("Article")]
        public long ArticleId { get; set; }

        public Article Article { get; set; }
        public Word Word { get; set; }
        public int Count { get; set; }
    }
}