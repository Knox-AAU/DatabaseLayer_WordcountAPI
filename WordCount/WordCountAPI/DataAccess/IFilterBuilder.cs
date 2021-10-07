using System;
using System.Collections.Generic;

namespace WordCount.DataAccess
{
    public interface IFilterBuilder<TEntity>
    {
        IOrderByBuilder<TEntity> Filter(Predicate<TEntity> filter);
        IEnumerable<TEntity> Execute();
    }
}