using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using WordCount.DataAccess;

namespace WordCount.Models
{
    [Table("appearsin")]
    public sealed class AppearsInModel : DatabaseEntityModel<int>
    {
        [Key]
        [Column("id", TypeName = "integer")]
        public int Id { get; }
        [Column("amount", TypeName = "integer")]
        public int Amount { get; set; }
        [Column("wordname", TypeName = "citext")]
        public string WordName { get; set; }
        [Column("filepath", TypeName = "text")]
        public string FilePath { get; set; }
        [Column("articletitle", TypeName = "text")]
        public string ArticleTitle { get; set; }

        public override int PrimaryKey => Id;
    }
}