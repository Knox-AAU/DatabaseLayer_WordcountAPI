using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code_first_test.Models
{
    public sealed class OccursIn
    {
        [Key]
        public long Id { get; set; }
        

        public Article Article { get; set; }
        public int Occurances { get; set; }
        

        public ICollection<Word> Words { get; set; }
    }
}