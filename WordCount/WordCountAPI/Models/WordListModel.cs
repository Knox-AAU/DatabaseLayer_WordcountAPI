using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    public sealed class WordListModel
    {
        [Key]
        [Column("wordname", TypeName = "citext")]
        public string WordName { get; set; }
    }
}