using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    public sealed class JsonSchemaModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("jsonbinary", TypeName = "jsonb")]
        public string JsonString { get; set; }
    }
}