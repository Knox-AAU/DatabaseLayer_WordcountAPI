using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public sealed class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : EntityModel
    {
        private readonly IQueryable<T> entities;

        public ReadOnlyRepository(DbContext context)
        {
            entities = context.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Find first entity with matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return entities.First(e => e.Id == id);
        }

        public IEnumerable<T> All()
        {
            return entities;
        }

        public T Find(Predicate<T> predicate)
        {
            return entities.First(e => predicate(e));
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
}