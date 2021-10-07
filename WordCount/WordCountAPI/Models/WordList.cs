using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WordCount.Models
{
    public sealed class WordList
    {
        [Key]
        [Column("wordname", TypeName = "citext")]
        public string WordName { get; set; }
    }
}