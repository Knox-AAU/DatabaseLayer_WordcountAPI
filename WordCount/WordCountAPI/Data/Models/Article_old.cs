using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordCount.Data.DataAccess;

namespace WordCount.Data.Models
{
    [Table("Article")]
    public sealed class Article_old : DatabaseEntityModel<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string FilePath { get; set; }
        public string Title { get; set; }
        public int TotalWords { get; set; }

        public Publisher Publisher { get; set; }
        public List<Term> Terms { get; set; }

        [NotMapped] public override long PrimaryKey => Id;
    }
}