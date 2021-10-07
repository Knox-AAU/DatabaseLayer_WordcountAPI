using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCount.DataAccess
{
    public interface IReadOnlyRepository<TEntity>
    {
        public TEntity GetById(int id);
        public IEnumerable<TEntity> All();
        public TEntity Find(Predicate<TEntity> predicate);
        public IEnumerable<TEntity> FindAll(Predicate<TEntity> predicate);

        public GetArranger<TEntity> Get ();
    }
}