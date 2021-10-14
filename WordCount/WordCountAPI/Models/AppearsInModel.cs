using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    [Table("appearsin")]
    public sealed class AppearsInModel
    {
        [Key]
        [Column("id", TypeName = "integer")]
        public int Id { get; set; }
        [Column("amount", TypeName = "integer")]
        public int Amount { get; set; }
        [Column("wordname", TypeName = "citext")]
        public string WordName { get; set; }
        [Column("filepath", TypeName = "text")]
        public string FilePath { get; set; }
        [Column("articletitle", TypeName = "text")]
        public string ArticleTitle { get; set; }
    }
}