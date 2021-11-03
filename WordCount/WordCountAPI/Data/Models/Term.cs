using System.ComponentModel.DataAnnotations.Schema;
using WordCount.Data.DataAccess;

namespace WordCount.Data.Models
{
    [Table("Term")]
    public sealed class Term : DatabaseEntityModel<CompositeKeyPair<long, string>>
    {
        public long ArticleId { get; set; }
        public string Word { get; set; }
        public int Count { get; set; }

        public override CompositeKeyPair<long, string> PrimaryKey =>
            new CompositeKeyPair<long, string>(ArticleId, Word);
    }
}