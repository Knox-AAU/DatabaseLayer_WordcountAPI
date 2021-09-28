namespace WordCount.JsonModels
{
    public sealed class ArticleData
    {
        public string articletitle { get; set; }
        public string filepath { get; set; }
        public int totalwordsinarticle { get; set; }
        public WordData[] words { get; set; }
    }
}