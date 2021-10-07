using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using WordCount.JsonModels;

namespace WordCount.DataAccess
{
    public interface IRepository<T>
    {
        public void Insert(T entity);
        public void Update(T employee);
        public void Delete(Predicate<T> predicate);
        public void Delete(T entity);
        public void Save();
        public IEnumerable<T> GetAll();
        public void Update(T entity, T oldData);

        public T Find(Predicate<T> predicate);

        public void SaveAsync();

        public IEnumerable<T> FindAll(Predicate<T> predicate);

        public IEnumerable<T> Get(Predicate<T> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    }
}