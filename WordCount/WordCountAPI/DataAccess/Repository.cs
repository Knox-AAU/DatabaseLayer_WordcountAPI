using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public sealed class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityModel
    {
        private readonly DbContext context;
        private readonly List<TEntity> entityList;

        public Repository(DbContext context)
        {
            this.context = context;
            entityList = context.Set<TEntity>().ToList();
        }

        public void Insert(TEntity entity)
        {
            entityList.Add(entity);
            Save();
        }
        
        public void Insert(IEnumerable<TEntity> entities)
        {
            entityList.AddRange(entities);
            Save();
        }
        
        public void Update(int oldEntityId, TEntity newEntity)
        {
            int index = entityList.FindIndex(entity => entity.Id == oldEntityId);

            if (index == -1)
            {
                // TODO: Proper logging
                Console.WriteLine($"No entity found with id {oldEntityId}.");
                return;
            }
            
            entityList[index] = newEntity;
            Save();
        }

        public void Update(TEntity oldEntity, TEntity newEntity)
        {
            Update(oldEntity.Id, newEntity);
        }
        
        public TEntity GetById(int id)
        {
            return entityList.Find(entity => entity.Id == id);
        }
        
        /// <summary>
        /// Deletes entity by references.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            entityList.Remove(entity);
            Save();
        }
        
        /// <summary>
        /// Delete first entity satisfying the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Predicate<TEntity> predicate)
        {
            entityList.Remove(Find(predicate));
            Save();
        }
        
        private void Save()
        {
            context.SaveChanges();
        }

        public IEnumerable<TEntity> All()
        {
            return entityList;
        }

        public TEntity Find(Predicate<TEntity> predicate)
        {
            return entityList.Find(predicate);
        }

        public IEnumerable<TEntity> FindAll(Predicate<TEntity> predicate)
        {
            return entityList.FindAll(predicate);
        }

        public void SaveAsync()
        {
            context.SaveChangesAsync();
        }

        public GetArranger<TEntity> Get()
        {
            return new GetArranger<TEntity>(entityList.AsQueryable());
        }
    }
}