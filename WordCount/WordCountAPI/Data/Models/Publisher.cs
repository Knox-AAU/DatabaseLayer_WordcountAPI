using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordCount.Data.Models;
using WordCount.DataAccess;

namespace WordCount.Models
{
    [Table("Publisher")]
    public sealed class Publisher : DatabaseEntityModel<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string PublisherName { get; set; }
        public override long PrimaryKey => Id;

        public List<Article> Articles { get; set; }
    }
}