using System;

namespace WordCount.Data.DataAccess
{
    public abstract class DatabaseEntityModel<TKey>
        where TKey : IEquatable<TKey>
    {
        public abstract TKey PrimaryKey { get; }
    }
}