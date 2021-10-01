using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    public class WordList
    {
        [Key]
        [Column(TypeName = "citext")]
        public string wordname { get; set; }
    }
}