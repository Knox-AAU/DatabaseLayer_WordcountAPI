using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    [Table("jsonschemas")]
    public sealed class JsonSchemaModel
    {
        [Key]
        [Column("schemaname")]
        public string SchemaName { get; set; }
        [Column("jsonbinary", TypeName = "jsonb")]
        public string JsonString { get; set; }
    }
}