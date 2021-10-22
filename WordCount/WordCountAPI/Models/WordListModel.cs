using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    [Table("wordlist")]
    public sealed class WordListModel
    {
        [Key]
        [Column("wordname", TypeName = "citext")]
        public string WordName { get; set; }
        
    }
}