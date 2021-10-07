using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCount.DataAccess
{
    public interface IRepository<TEntity> where TEntity : EntityModel
    {
        public void Insert(IEnumerable<TEntity> entities);
        public void Update(int id, TEntity newEntity);
        public TEntity GetById(int id);

        /// <summary>
        /// Deletes entity by references.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity);

        public void Insert(TEntity entity);

        /// <summary>
        /// Delete first entity satisfying the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Predicate<TEntity> predicate);
        
        public IEnumerable<TEntity> All();
        public TEntity Find(Predicate<TEntity> predicate);
        public IEnumerable<TEntity> FindAll(Predicate<TEntity> predicate);

        public GetArranger<TEntity> Get();
    }
}