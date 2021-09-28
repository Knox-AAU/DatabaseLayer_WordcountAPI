namespace KnoxDatabaseLayer3.JsonModels
{
    public interface IPostRoot<out T>
    {
        T[] Data { get; }
    }
}