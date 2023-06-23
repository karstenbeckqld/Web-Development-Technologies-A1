using System.Reflection;

namespace A1ClassLibrary.Utils;
// The TypeExtensions class provides an extension method to filter properties when using a generics approach to handle
// database access. Because the models contain properties that are not represented in the database tables (eg. Customer
// has Accounts and Login) and this leads to NullPointerExceptions, we use the extension method in the class that handles
// all database access. 
public static class TypeExtensions
{
    public static PropertyInfo[] GetFilteredProperties(this Type type)
    {
        return type.GetProperties()
            .Where(propertyInfo => !propertyInfo.IsDefined(typeof(SkipPropertyAttribute)))
            .ToArray();
    } 
}