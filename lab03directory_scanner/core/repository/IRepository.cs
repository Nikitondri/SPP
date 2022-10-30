namespace core.repository;

public interface IRepository<T>
{
    void Add(T value);
    T FindById(int id);
}