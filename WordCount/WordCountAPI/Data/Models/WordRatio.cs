namespace WordCount.Data.Models
{
    public class WordRatio
    {
        public long ArticleId { get; set; }
        public string Word { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public int TotalWords { get; set; }
        public string PublisherName { get; set; }
        public float Percent { get; set; }
    }
}