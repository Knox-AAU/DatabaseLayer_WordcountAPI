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
        private ICollection<T> _entitiesSet;

        public ReadOnlyRepository(DbContext context)
        {
            _entitiesSet = context.Set<T>().ToList();
        }

        public ReadOnlyRepository(DbSet<T> entitySet)
        {
            _entitiesSet = entitySet.ToList();
        }
        
        public ReadOnlyRepository(ICollection<T> entities)
        {
            _entitiesSet = entities;
        }


        /// <summary>
        /// Find first entity with matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(TKey key)
        {
            return _entitiesSet.First(a => a.PrimaryKey.Equals(key));
        }

        public IEnumerable<T> All()
        {
            return _entitiesSet;
        }

        public T Find(Predicate<T> predicate)
        {
            try
            {
                return _entitiesSet.First(e => predicate(e));
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
                return _entitiesSet.ToList().First(e=> e.PrimaryKey.Equals(entity.PrimaryKey));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<T> FindAll(Predicate<T> predicate)
        {
            return _entitiesSet.Where(e => predicate(e));
        }

        public GetArranger<T> Get()
        {
            return new GetArranger<T>(_entitiesSet.AsQueryable());
        }
    }
}