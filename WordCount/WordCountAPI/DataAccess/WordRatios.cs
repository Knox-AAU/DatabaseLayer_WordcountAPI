using System.ComponentModel.DataAnnotations.Schema;

namespace WordCount.DataAccess
{
    [Table("wordratios")]
    public class WordRatios
    {
        [Column("id", TypeName = "integer")]
        public int Id { get; set; }
        [Column("wordname", TypeName = "citext")]
        public string WordName { get; set; }
        [Column("amount", TypeName = "integer")]
        public int Amount { get; set; }
        [Column("articletitle", TypeName = "text")]
        public string ArticleTitle { get; set; }
        [Column("filepath", TypeName = "text")]
        public string FilePath { get; set; }
        [Column("totalwordsinarticle", TypeName = "integer")]
        public int TotalWordsInArticle { get; set; }
        [Column("sourceid", TypeName = "bigint")]
        public int SourceId { get; set; }
        [Column("sourcename", TypeName = "varchar")]
        public string SourceName { get; set; }
        [Column("percent", TypeName = "numeric")]
        public float Percent { get; set; }
    }
}