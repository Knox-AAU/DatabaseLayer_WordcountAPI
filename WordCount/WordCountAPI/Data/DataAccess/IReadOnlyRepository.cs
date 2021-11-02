using System;
using System.Collections.Generic;

namespace WordCount.Data.DataAccess
{
    public interface IReadOnlyRepository<TEntity, in TKey> 
        where TEntity : DatabaseEntityModel<TKey> where TKey : IEquatable<TKey>
    {
        public TEntity GetById(TKey id);
        public List<TEntity> All();
        /// <summary>
        /// Finds first entity and returns it
        /// </summary>
        /// <param name="predicate"></param>
        
        public TEntity Find(Predicate<TEntity> predicate);
        public IEnumerable<TEntity> FindAll(Predicate<TEntity> predicate);
        public GetArranger<TEntity> Find ();
        
    }
}