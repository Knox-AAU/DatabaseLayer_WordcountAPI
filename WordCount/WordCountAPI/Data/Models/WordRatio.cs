using System;
using System.Collections.Generic;

#nullable disable

namespace WordCount
{
    public partial class WordRatio
    {
        public long? ArticleId { get; set; }
        public string Word { get; set; }
        public int? Count { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public int? TotalWords { get; set; }
        public string PublisherName { get; set; }
        public decimal? Percent { get; set; }
    }
}
