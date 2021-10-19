using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.Models
{
    [Table("filelist")]
    public sealed class FileListModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", TypeName = "bigint")]
        public int Id { get; set; }
        [Column("filepath", TypeName = "text")]
        public string FilePath { get; set; }
        [Column("articletitle", TypeName = "text")]
        public string ArticleTitle { get; set; }
        [Column("totalwordsinarticle", TypeName = "integer")]
        public int TotalWordsInArticle { get; set; }
        [Column("sourceid", TypeName = "bigint")]
        public long SourceId { get; set; }
    }
}