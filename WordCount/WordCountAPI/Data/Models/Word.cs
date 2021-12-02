using System.ComponentModel.DataAnnotations;

namespace WordCount.Data.Models
{
    public sealed class Word
    {
        [Key]
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