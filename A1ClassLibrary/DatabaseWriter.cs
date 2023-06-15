using s3893749_s3912792_a1.interfaces;

namespace A1ClassLibrary;

public class DatabaseWriter
{
    public DatabaseWriter(IDatabaseObject data)
    {
        var fieldValues = data.GetType().GetFields().Select(field => field.GetValue(data)).ToList();
    }

    public bool Insert()
    {
        return false;
    }

    public bool Update()
    {
        return false;
    }

    public bool Delete()
    {
        return false;
    }
}