using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace WordCount.Data.DataAccess
{
    public class ReadOnlyRepository<T, TKey> : IReadOnlyRepository<T, TKey>
        where T : DatabaseEntityModel<TKey>
        where TKey : IEquatable<TKey>
    {
        public IReadOnlyList<T> EntitySet => InternalEntitySet;
        protected List<T> InternalEntitySet { get; }

        public ReadOnlyRepository(ArticleContext context)
        {
            InternalEntitySet = context.Set<T>().ToList();
        }

        public ReadOnlyRepository(DbSet<T> entitySet)
        {
            if (entitySet == null)
            {
                throw new NullReferenceException();
            }

            InternalEntitySet = entitySet.ToList();
        }

        public ReadOnlyRepository(List<T> internalEntity)
        {
            if (internalEntity == null)
            {
                throw new NullReferenceException();
            }

            InternalEntitySet = internalEntity;
        }

        /// <summary>
        /// Find first entity with matching id
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetById(TKey key)
        {
            try
            {
                return InternalEntitySet.First(a => a.PrimaryKey.Equals(key));
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all elements of the entities
        /// </summary>
        /// <returns>All entities</returns>
        public List<T> All()
        {
            return InternalEntitySet;
        }

        /// <summary>
        /// Find the first entity satisfying the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Entity satisfying predicate</returns>
        public T Find(Predicate<T> predicate)
        {
            try
            {
                return InternalEntitySet.First(e => predicate(e));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Finds entity with equal primary key
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Find(T entity)
        {
            try
            {
                return InternalEntitySet.ToList().First(e => e.PrimaryKey.Equals(entity.PrimaryKey));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Finds all elements satisfying predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>All entities satisfying the predicate</returns>
        public IEnumerable<T> FindAll(Predicate<T> predicate)
        {
            return InternalEntitySet.Where(e => predicate(e));
        }

        public GetArranger<T> Find()
        {
            return new GetArranger<T>(InternalEntitySet.AsQueryable());
        }
    }
}