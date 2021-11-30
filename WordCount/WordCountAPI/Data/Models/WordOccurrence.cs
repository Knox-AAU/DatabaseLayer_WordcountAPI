namespace WordCount.Data.Models
{
    public sealed class WordOccurrence
    {
        public long ArticleId { get; set; }
        public Article Article { get; set; }
        public Word Word { get; set; }
        public int Count { get; set; }
    }
}