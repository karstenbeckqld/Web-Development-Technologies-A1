namespace s3893749_s3912792_a1.interfaces;

public interface IManager<T>
{
    public abstract void Insert(T data);

    public abstract List<T> GetAll();

    public abstract void Update(T data);

    public abstract List<T> Get(int value);
}