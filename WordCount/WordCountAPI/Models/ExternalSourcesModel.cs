using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    [Table("externalsources")]
    public sealed class ExternalSourcesModel
    {
        [Key]
        [Column("id", TypeName = "bigint")]
        public long Id { get; set; }
        [Column("sourcename", TypeName = "varchar")]
        public string SourceName { get; set; }
    }
}