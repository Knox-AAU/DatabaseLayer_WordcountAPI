using System;

namespace WordCount.JsonModels
{
    public sealed class WordData : IEquatable<WordData>
    {
        public int Amount { get; set; }
        public string Word { get; set; }


        public bool Equals(WordData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount && Word == other.Word;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is WordData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Word);
        }
    }
}