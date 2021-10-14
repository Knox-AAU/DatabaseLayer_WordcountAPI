using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    [Table("json_schema")]
    public sealed class JsonSchemaModel
    {
        [Key]
        [Column("schema_name")]
        public string SchemaName { get; set; }
        [Column("json_binary", TypeName = "jsonb")]
        public string JsonString { get; set; }
    }
}