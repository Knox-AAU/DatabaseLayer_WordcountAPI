using System.Collections.Generic;

namespace WordCount.Data.Models
{
    public class Word
    {
        public string Literal { get; set; }
        public List<Article> Articles { get; set; }
    }
}