using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Internal;
using WordCount.Data;
using WordCount.Models;

namespace WordCount.DataAccess
{
    public class ReadOnlyRepository<T, TKey> : IReadOnlyRepository<T, TKey> 
        where T : DatabaseEntityModel<TKey> 
        where TKey : IEquatable<TKey>
    {
        public IReadOnlyList<T> EntitySet => InternalEntitySet;
        protected List<T> InternalEntitySet { get; }

        public ReadOnlyRepository(DbContext context)
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
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(TKey key)
        {
            return InternalEntitySet.First(a => a.PrimaryKey.Equals(key));
        }

        public IEnumerable<T> All()
        {
            return InternalEntitySet;
        }

        public T Find(Predicate<T> predicate)
        {
            try
            {
                return InternalEntitySet.First(e => predicate(e));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        public T Find(T entity)
        {
            try
            {
                return InternalEntitySet.ToList().First(e=> e.PrimaryKey.Equals(entity.PrimaryKey));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<T> FindAll(Predicate<T> predicate)
        {
            return InternalEntitySet.Where(e => predicate(e));
        }

        public GetArranger<T> Get()
        {
            return new GetArranger<T>(InternalEntitySet.AsQueryable());
        }
    }
}