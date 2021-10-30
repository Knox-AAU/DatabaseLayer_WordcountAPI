using Microsoft.Data.Entity;
using WordCount.Data;
using WordCount.Data.Models;
using WordCount.Models;

namespace WordCount.DataAccess
{
    public class UnitOfWork
    {
        private ArticleContext context;
        private Repository<Article, long> articleRepository;
        private Repository<JsonSchemaModel, string> schemaRepository;
        public IRepository<JsonSchemaModel,string> SchemaRepository 
        {
            get
            {
                if (schemaRepository == null)
                {  
                    schemaRepository = new Repository<JsonSchemaModel, string>(context);
                }

                return schemaRepository;
            }
            
        }

        private Repository<WordRatio, CompositeKeyTriple<long, long, string>> wordRatioRepository;

        public IRepository<WordRatio, CompositeKeyTriple<long, long, string>> WordRatioRepository
        {
            get
            {
                if (wordRatioRepository == null)
                {  
                    wordRatioRepository = new Repository<WordRatio, CompositeKeyTriple<long, long, string>>(context);
                }

                return wordRatioRepository;
            }
        }

        
        public IRepository<Article, long> ArticleRepository
        {
            get
            {
                if (articleRepository == null)
                {
                    articleRepository = new Repository<Article, long>(context);
                }

                return articleRepository;
            }
        }

        public Repository<Publisher, string> publisherRepository;

        public IRepository<Publisher, string> PublisherRepository 
        {
            get
            {
                if (publisherRepository == null)
                {
                    publisherRepository = new Repository<Publisher, string>(context);
                }

                return publisherRepository;
            }  
        }

        
        public UnitOfWork(ArticleContext context)
        {
            this.context = context;
        }

        public UnitOfWork()
        {
            context = new ArticleContext();
        }
    }
}