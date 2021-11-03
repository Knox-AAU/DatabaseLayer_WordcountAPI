using System;
using System.Collections.Generic;

namespace WordCount.Data.DataAccess
{
    public interface IRepository<TEntity, in TKey> where TEntity : DatabaseEntityModel<TKey> where TKey : IEquatable<TKey>
    {
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates existing entity in internal list and invokes functions subscribed to ListChanged event.
        /// ListChanged.
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="newEntity"></param>
        /// <exception cref="ArgumentException"></exception>
        void Update(TKey entityKey, TEntity newEntity);

        /// <summary>
        /// Updates existing entity in internal list and invokes functions subscribed to ListChanged event.
        /// ListChanged.
        /// </summary>
        /// <param name="oldEntity"></param>
        /// <param name="newEntity"></param>
        /// <exception cref="ArgumentException"></exception>
        void Update(TEntity oldEntity, TEntity newEntity);

        void Remove(TEntity entities);
        void Remove(IEnumerable<TEntity> entities);
        bool TryGetEntity(TEntity entity, out TEntity existingEntity);
        IReadOnlyList<TEntity> EntitySet { get; }

        /// <summary>
        /// Find first entity with matching id
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity GetById(TKey key);

        /// <summary>
        /// Get all elements of the entities
        /// </summary>
        /// <returns>All entities</returns>
        List<TEntity> All();

        /// <summary>
        /// Find the first entity satisfying the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Entity satisfying predicate</returns>
        TEntity Find(Predicate<TEntity> predicate);

        /// <summary>
        /// Finds entity with equal primary key
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Find(TEntity entity);

        GetArranger<TEntity> Find();

        /// <summary>
        /// Finds all elements satisfying predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>All entities satisfying the predicate</returns>
        IEnumerable<TEntity> FindAll(Predicate<TEntity> predicate);

        event Action ListChanged;

        /// <summary>
        /// Insert the entity into the internal list, unless already existing and invokes functions subscribed to
        /// ListChanged.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentException"></exception>
        void Insert(TEntity entity);

        /// <summary>
        /// Insert the entity into the internal list, unless already existing and invokes functions subscribed to
        /// ListChanged event.
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="ArgumentException"></exception>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes entity by references.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete first entity satisfying the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Predicate<TEntity> predicate);
    }
}