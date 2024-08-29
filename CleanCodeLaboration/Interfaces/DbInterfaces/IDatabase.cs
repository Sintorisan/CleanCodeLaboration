namespace CleanCodeLaboration.Interfaces.DbInterfaces;

public interface IDatabase<T>
{
    void InitialLoad();

    void Add(T item);

    List<T> GetAll();
}