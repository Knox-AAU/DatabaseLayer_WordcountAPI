using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordCount.DataAccess;

namespace WordCount.Models
{
    [Table("externalsources")]
    public sealed class ExternalSourcesModel:DatabaseEntityModel<long>
    {
        [Key]
        [Column("id", TypeName = "bigint")]
        public long Id { get; set; }
        [Column("sourcename", TypeName = "varchar")]
        public string SourceName { get; set; }
        public override long PrimaryKey => Id;
    }
}