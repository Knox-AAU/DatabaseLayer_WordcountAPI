using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WordCount.Data.Models;


namespace WordCount.DataAccess
{
    [Table("WordRatio")]
    
    public class WordRatio : DatabaseEntityModel<CompositeKeyTriple<long,long,string>>
    {
        public long ArticleId { get; set; }
        public string Word { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public int TotalWords { get; set; }
        public long PublisherId { get; set; }
        public string PublisherName { get; set; }
        [Column("percent")]
        public float Percent { get; set; }
        
        [JsonIgnore]
        public override CompositeKeyTriple<long, long, string> PrimaryKey =>
            new CompositeKeyTriple<long, long, string>(ArticleId, PublisherId, Word);
    }
}