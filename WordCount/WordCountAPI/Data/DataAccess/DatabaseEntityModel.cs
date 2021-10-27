using System;

namespace WordCount.DataAccess
{
    public abstract class DatabaseEntityModel<TKey> where TKey : IEquatable<TKey>
    {
        public abstract TKey PrimaryKey { get; }
    }
}