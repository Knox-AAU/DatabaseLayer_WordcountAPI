using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Data.Models
{
    [Table("JsonSchema")]
    public sealed class JsonSchemaModel
    {
        [Key]
        public string SchemaName { get; set; }
        [Column(TypeName = "jsonb")]
        public string JsonString { get; set; }
    }
}