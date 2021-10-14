using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordCount.Controllers;
using WordCount.DataAccess;

namespace WordCount.Models
{
    [Table("json_schema")]
    public sealed class JsonSchema : DatabaseEntityModel<string>
    {
        [Key]
        [Column("schema_name")]
        public string SchemaName { get; set; }
        [Column("json_binary", TypeName = "jsonb")]
        public string JsonString { get; set; }

        public override string PrimaryKey => SchemaName; 
    }
}