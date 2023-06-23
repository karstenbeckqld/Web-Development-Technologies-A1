namespace A1ClassLibrary.Interfaces;

public interface IManager<T>
{
    public void Insert(T data);

    public List<T> GetAll();

    public void Update(T data);

    public List<T> Get(int value);
}