using System;
using System.Collections.Generic;

#nullable disable

namespace WordCount
{
    public partial class OccursIn
    {
        public long ArticleId { get; set; }
        public int? Count { get; set; }
        public string Word { get; set; }

        public virtual Article Article { get; set; }
        public virtual Word WordNavigation { get; set; }
    }
}
