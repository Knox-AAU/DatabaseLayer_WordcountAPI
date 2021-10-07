using System;
using System.Linq;

namespace WordCount.JsonModels
{
    public sealed class Article : IEquatable<Article>
    {
        public string ArticleTitle { get; set; }
        public string FilePath { get; set; }
        public int TotalWordsInArticle { get; set; }
        public WordData[] Words { get; set; }


        public bool Equals(Article other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ArticleTitle == other.ArticleTitle && FilePath == other.FilePath && TotalWordsInArticle == other.TotalWordsInArticle && Equals(Words, other.Words);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Article other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ArticleTitle, FilePath, TotalWordsInArticle, Words);
        }
    }
}