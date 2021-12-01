using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Data.Models
{
    [Table("Word")]
    public class Word
    {
        [Key]
        public string Literal { get; set; }
    }
}