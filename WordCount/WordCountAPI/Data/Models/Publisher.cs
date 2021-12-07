using System;
using System.Collections.Generic;

#nullable disable

namespace WordCount
{
    public partial class Publisher
    {
        public Publisher()
        {
            Articles = new HashSet<Article>();
        }

        public string PublisherName { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
