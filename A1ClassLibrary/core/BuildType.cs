namespace A1ClassLibrary.core;

public class BuildType
{
    public BuildType(Type type)
    {
        var properties = type.GetProperties();

        var obj = Activator.CreateInstance(type);

        foreach (var property in properties)
        {
            property.SetValue(obj,property.Name);
        }
        
        Console.WriteLine(obj);
    }
}