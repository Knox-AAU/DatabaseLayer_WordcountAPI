namespace KnoxDatabaseLayer3.JsonUtility
{
    public sealed class WordCountPostRoot
    {
        public ArticleData[] Articles { get; set; }
    }

    public sealed class ArticleData
    {
        public string articletitle { get; set; }
        public string filepath { get; set; }
        public int totalwordsinarticle { get; set; }
        public WordData[] words { get; set; }
    }

    public sealed class WordData
    {
        public int amount { get; set; }
        public string word { get; set; }
    }
}