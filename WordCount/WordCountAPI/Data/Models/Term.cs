namespace WordCount.Data.Models
{
    public sealed class HasWord
    {
        public long ArticleId { get; set; }

        public Word Word { get; set; }
        public int Count { get; set; }
    }
}