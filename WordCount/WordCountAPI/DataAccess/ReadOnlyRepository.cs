using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public sealed class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : EntityModel
    {
        private readonly IQueryable<T> _entities;

        public ReadOnlyRepository(DbContext context)
        {
            _entities = context.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Find first entity with matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return _entities.First(e => e.Id == id);
        }

        public IEnumerable<T> All()
        {
            return _entities;
        }

        public T Find(Predicate<T> predicate)
        {
            return _entities.First(e => predicate(e));
        }

        public IEnumerable<T> FindAll(Predicate<T> predicate)
        {
            return _entities.Where(e => predicate(e));
        }

        public IEnumerable<T> Get(Predicate<T> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _entities;
            if (filter != null)
            {
                query = query.Where(e => filter(e));
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }
    }
}