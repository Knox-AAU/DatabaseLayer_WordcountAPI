using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WordCount.Data.Models
{
    [Table("Publisher")]
    [Index(nameof(PublisherName), IsUnique = true)]
    public sealed class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PublisherName { get; set; }
        public string PrimaryKey => PublisherName;

        public List<Article> Articles { get; set; }
    }
}