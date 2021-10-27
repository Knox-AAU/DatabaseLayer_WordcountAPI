using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WordCount.Models
{
    public class CompositeKeyPair<T1, T2 > : IEquatable<CompositeKeyPair<T1,T2>>
    {
        public T1 First { get; }
        public T2 Second { get; }
        public bool Equals(CompositeKeyPair<T1, T2> other)
        {
            if (other == null) return false;
            return this.First.Equals(other.First) && this.Second.Equals(other.Second);
        }
    
        public CompositeKeyPair([NotNull]T1 first, [NotNull]T2 second)
        {
            First = first;
            Second = second;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CompositeKeyPair<T1, T2>) obj);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 851153;
                hash = hash * 228601 + First.GetHashCode();
                hash = hash * 826151 + Second.GetHashCode();
                return hash;
            }
        }
    }

    
}