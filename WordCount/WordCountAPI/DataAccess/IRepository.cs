using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCount.DataAccess
{
    public interface IRepository<TEntity, in TKey> : IReadOnlyRepository<TEntity, TKey>
        where TEntity : DatabaseEntityModel<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Insert and each of the entities
        /// </summary>
        /// <param name="entities"></param>
        public void Insert(IEnumerable<TEntity> entities);
        
        /// <summary>
        /// Updates entity in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newEntity"></param>
        public void Update(TKey id, TEntity newEntity);

        /// <summary>
        /// Deletes entity by references.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity);

        /// <summary>
        /// Inserts and saves entity to database
        /// </summary>
        /// <param name="entity"></param>

        public void Insert(TEntity entity);

        /// <summary>
        /// Delete first entity satisfying the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Predicate<TEntity> predicate);


    }
}