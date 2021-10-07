using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    public sealed class ExternalSourcesModel
    {
        [Key]
        [Column("id", TypeName = "bigint")]
        public long Id { get; set; }
        [Column("id", TypeName = "varchar")]
        public string SourceName { get; set; }
    }
}