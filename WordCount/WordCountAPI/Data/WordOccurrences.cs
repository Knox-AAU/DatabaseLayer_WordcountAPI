using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code_first_test.Models
{
    public sealed class WordOccurrences
    {
        [Key]
        public long WordOccurrencesId { get; set; }
        public Article Article { get; set; }
        public int Occurances { get; set; }
        public ICollection<Word> Words { get; set; }
    }
}