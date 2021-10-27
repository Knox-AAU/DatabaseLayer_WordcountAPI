﻿using System.ComponentModel.DataAnnotations;

namespace WordCount.JsonModels
{
    public sealed class Article
    {
        public long ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string Publication { get; set; }
        public string FilePath { get; set; }
        public int TotalWordsInArticle { get; set; }
        public WordData[] Words { get; set; }
    }
}