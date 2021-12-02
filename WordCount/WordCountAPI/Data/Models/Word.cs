using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Data.Models
{
    public sealed class Word
    {
        [Column(TypeName = "citext")]
        public string Text { get; set; }

        public Word()
        {
            
        }

        public Word(string text)
        {
            Text = text;
        }
    }
}