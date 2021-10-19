using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code_first_test.Models
{
    public abstract class Publisher
    {
        [Key]
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}