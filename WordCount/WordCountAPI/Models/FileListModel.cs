using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordCount.DataAccess;

namespace WordCount.Models
{
    [Table("filelist")]
    public sealed class FileListModel : DatabaseEntityModel<int>
    {
        [Key]
        [Column("id", TypeName = "bigint")]
        public int Id { get;}
        [Column("filepath", TypeName = "text")]
        public string FilePath { get; set; }
        [Column("articletitle", TypeName = "text")]
        public string ArticleTitle { get; set; }
        [Column("totalwordsinarticle", TypeName = "integer")]
        public int TotalWordsInArticle { get; set; }
        [Column("sourceid", TypeName = "bigint")]
        public long SourceId { get; set; }

        public override int PrimaryKey => Id;
    }
}