using System;
using Microsoft.Data.Entity;
using WordCount.JsonModels;

namespace WordCount.DataAccess
{
    public class UnitOfWork : IDisposable
    {   
        private DbContext context;
        private Repository<Article> ArticleRepository { get; set; }
        private Repository<WordData> WordRepository { get; set; }



        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public UnitOfWork(Repository<Article> articleRepository, Repository<WordData> wordRepository, DbContext context)
        {
            ArticleRepository = articleRepository;
            WordRepository = wordRepository;
            this.context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}