using WordCount.Data.Models;

namespace WordCount.Data.DataAccess
{
    public class UnitOfWork : IUnitOfWork
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

        private Repository<WordRatio, CompositeKeyTriple<long, string, string>> wordRatioRepository;

        public IRepository<WordRatio, CompositeKeyTriple<long, string, string>> WordRatioRepository
        {
            get
            {
                if (wordRatioRepository == null)
                {  
                    wordRatioRepository = new Repository<WordRatio, CompositeKeyTriple<long, string, string>>(context);
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

        private Repository<Publisher, string> publisherRepository;

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