using System;
using System.Linq;
using WordCount.DataAccess;

namespace WordCount.JsonModels
{
    public sealed class Article
    {
        public string ArticleTitle { get; set; }
        public string FilePath { get; set; }
        public int TotalWordsInArticle { get; set; }
        public WordData[] Words { get; set; }



    }
}