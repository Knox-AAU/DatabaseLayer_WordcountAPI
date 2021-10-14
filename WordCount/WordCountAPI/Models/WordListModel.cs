using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordCount.DataAccess;

namespace WordCount.Models
{
    [Table("wordlist")]
    public sealed class WordListModel : DatabaseEntityModel<string>
    {
        [Key]
        [Column("wordname", TypeName = "citext")]
        public string WordName { get; set; }

        public override string PrimaryKey => WordName;
    }
}