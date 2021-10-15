using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code_first_test.Models
{
    public sealed class Word
    {
        [Key]
        public string Literal { get; set; }
    }

    public sealed class WordOccurances
    {
        [Key]
        public long WordOccuranceId { get; set; }
        public Article Article { get; set; }
        public int Occurances { get; set; }
        public ICollection<Word> Words { get; set; }
    }
}