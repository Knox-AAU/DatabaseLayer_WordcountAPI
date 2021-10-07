using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using WordCount.JsonModels;

namespace WordCount.DataAccess
{
    public interface IRepository<T>
    {
        void Insert(T employee);
        void Update(T employee);
        void Delete(Predicate<T> predicate);
        void Save();
        IEnumerable<T> GetAll();
        T GetById(Predicate<T> predicate);

        T Find(Predicate<T> predicate);
    }
}