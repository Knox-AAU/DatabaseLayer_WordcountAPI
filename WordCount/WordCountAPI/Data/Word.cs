using System.ComponentModel.DataAnnotations;

namespace Code_first_test.Models
{
    public sealed class Word
    {
        [Key]
        public string Literal { get; set; }
    }
}