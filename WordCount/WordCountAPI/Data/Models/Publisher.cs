using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WordCount.Data.DataAccess;

namespace WordCount.Data.Models
{
    [Table("Publisher")]
    [Index(nameof(PublisherName), IsUnique = true)]
    public sealed class Publisher : DatabaseEntityModel<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PublisherName { get; set; }
        public override string PrimaryKey => PublisherName;

        public List<Article> Articles { get; set; }
    }
}