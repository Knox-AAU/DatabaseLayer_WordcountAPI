using WordCount.Data.Models;

namespace WordCount.Data.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<Article, long> ArticleRepository { get; }
        IRepository<JsonSchemaModel, string> SchemaRepository { get; }
        IRepository<WordRatio, CompositeKeyTriple<long, string, string>> WordRatioRepository { get; }
        IRepository<Publisher, string> PublisherRepository { get; }
    }
}