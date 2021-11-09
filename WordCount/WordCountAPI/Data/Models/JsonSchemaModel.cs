using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordCount.Data.DataAccess;

namespace WordCount.Data.Models
{
    [Table("JsonSchema")]
    public sealed class JsonSchemaModel : DatabaseEntityModel<string>
    {
        [Key]
        public string SchemaName { get; set; }
        [Column(TypeName = "jsonb")]
        public string JsonString { get; set; }

        public override string PrimaryKey => SchemaName;
    }
}