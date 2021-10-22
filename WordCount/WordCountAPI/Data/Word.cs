using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code_first_test.Models
{
    public sealed class Word
    {
        [Key]
        public string Literal { get; set; }
        public ICollection<OccursIn> OccursIn { get; set; }
    }
}