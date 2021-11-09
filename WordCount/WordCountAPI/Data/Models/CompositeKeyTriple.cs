using System;
using System.Diagnostics.CodeAnalysis;

namespace WordCount.Data.Models
{
        public class CompositeKeyTriple<T1, T2, T3> : CompositeKeyPair<T1, T2>, IEquatable<CompositeKeyTriple<T1, T2, T3>>
        {
            public CompositeKeyTriple([NotNull]T1 first, [NotNull] T2 second, [NotNull] T3 third) : base(first, second)
            {
                Third = third;
            }

            public T3 Third { get; set; }

            public bool Equals(CompositeKeyTriple<T1, T2, T3> other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return base.Equals(other) && this.Third.Equals(other.Third);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((CompositeKeyTriple<T1, T2, T3>)obj);
            }

            public override int GetHashCode()
            {
                int hash = 851153;
                hash = hash * 228601 + First.GetHashCode();
                hash = hash * 826151 + Second.GetHashCode();
                hash = hash * 38299 + Second.GetHashCode();
                return hash;
            }
        }
}