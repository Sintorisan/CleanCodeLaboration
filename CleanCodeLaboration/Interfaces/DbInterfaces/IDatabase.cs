namespace CleanCodeLaboration.Interfaces.DbInterfaces;

public interface IDatabase<T>
{
    void InitialLoad();

    bool Add(T item);

    List<T> GetAll();
}