namespace WordCount.Models
{
    public sealed class AppearsInModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string WordName { get; set; }
        public string FilePath { get; set; }
        public string ArticleTitle { get; set; }
    }
}