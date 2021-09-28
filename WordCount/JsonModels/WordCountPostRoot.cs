namespace KnoxDatabaseLayer3.JsonModels
{
    public sealed class WordCountPostRoot : IPostRoot<ArticleData>
    {
        public ArticleData[] Data { get; set; }
    }
}