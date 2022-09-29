using System;
using System.Collections.Generic;

#nullable disable

namespace WordCount
{
    public partial class Word
    {
        public Word()
        {
            OccursIns = new HashSet<OccursIn>();
        }

        public Word(string text)
        {
            OccursIns = new HashSet<OccursIn>();
            Text = text;
        }

        public string Text { get; set; }

        public virtual ICollection<OccursIn> OccursIns { get; set; }
    }
}
