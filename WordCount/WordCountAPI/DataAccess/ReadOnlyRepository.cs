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
        private DbSet<T> entities;

        public ReadOnlyRepository(DbContext context)
        {
            entities = context.Set<T>();
        }

        /// <summary>
        /// Find first entity with matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(TKey key)
        {
            return entities.First(a => a.PrimaryKey.Equals(key));
        }

        public IEnumerable<T> All()
        {
            return entities;
        }

        public T Find(Predicate<T> predicate)
        {

            try
            {
                return entities.First(e => predicate(e));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<T> FindAll(Predicate<T> predicate)
        {
            return entities.Where(e => predicate(e));
        }

        public GetArranger<T> Get()
        {
            return new GetArranger<T>(entities);
        }
    }

    public abstract class DatabaseEntityModel<TKey> where TKey : IEquatable<TKey>
    {
        public abstract TKey PrimaryKey { get; }
    }
}