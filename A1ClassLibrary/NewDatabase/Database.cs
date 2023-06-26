using A1ClassLibrary.attributes;
using A1ClassLibrary.Utils;
using EasyDB;
using EasyDB.attributes;
using Microsoft.Data.SqlClient;
using A1ClassLibrary.NewDatabase;

namespace A1ClassLibrary.NewDatabase;

public class Database<T>
{
    private Dictionary<string, string> _where;
    private List<T> _output;

    public Database()
    {
        _where = new Dictionary<string, string>();
        _output = new List<T>();
    }

    public Database<T> Where(string key, string value)
    {
        _where.Add(key, value);
        return this;
    }

    public List<T> GetAll()
    {
        string table = typeof(T).Name;
        string query = "SELECT * FROM " + table;

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        if (_where.Count > 0)
        {
            query += " WHERE ";

            foreach (var keyValuePair in _where)
            {
                query += keyValuePair.Key + "=" + "@" + keyValuePair.Key + ",";
                parameters.Add("@" + keyValuePair.Key, keyValuePair.Value);
            }

            query = query.TrimEnd(',');
        }

        DatabaseResponse databaseResponse = DatabaseQueryExecutor.Query(query, parameters);

        foreach (var row in databaseResponse.GetRows())
        {
            T obj = Activator.CreateInstance<T>();

            var objectType = obj.GetType();

            var pInfo = objectType.GetFilteredProperties();

            foreach (var property in pInfo)
            {
                if (row[property.Name] != null)
                {
                    property.SetValue(obj, Convert.ChangeType(row[property.Name], property.PropertyType));
                }
                else
                {
                    property.SetValue(obj, null);
                }
            }

            _output.Add(obj);
        }

        return _output;
    }

    public dynamic GetFirst()
    {
        GetAll();

        if (_output.Count > 0)
        {
            return _output[0];
        }
        else
        {
            return null;
        }
    }

    public static bool Insert(T model)
    {
        string table = model.GetType().Name;
        string query = "INSERT INTO " + table + " (";
        string values = "(";
        Dictionary<string, string> parameters = new Dictionary<string, string>();

        foreach (var fieldInfo in model.GetType().GetProperties())
        {
            query += fieldInfo.Name + ",";
            values += "@" + fieldInfo.Name + ",";
            parameters.Add("@" + fieldInfo.Name,
                model.GetType().GetProperty(fieldInfo.Name).GetValue(model).ToString());
        }

        query = query.TrimEnd(',');
        query += ") VALUES ";

        values = values.TrimEnd(',');
        query += values;
        query += ")";

        return DatabaseQueryExecutor.Query(query, parameters).GetOutcome();
    }

    public static bool Update(T model)
    {
        string table = model.GetType().Name;
        string query = "UPDATE " + table + " SET ";
        string primaryKey = "";

        Dictionary<string, string> parameters = new Dictionary<string, string>();


        foreach (var fieldInfo in model.GetType().GetProperties())
        {
            if (!Attribute.IsDefined(fieldInfo, typeof(SkipPropertyAttribute)))
            {
                if (Attribute.IsDefined(fieldInfo, typeof(PrimaryKeyAttribute)))
                {
                    primaryKey = fieldInfo.Name;
                }

                query += fieldInfo.Name + "=@" + fieldInfo.Name + ",";
                parameters.Add("@" + fieldInfo.Name,
                    model.GetType().GetProperty(fieldInfo.Name).GetValue(model).ToString());
            }
        }

        query = query.TrimEnd(',');

        query += " WHERE " + primaryKey + "=@" + primaryKey;

        Console.WriteLine(query);
        return DatabaseQueryExecutor.Query(query, parameters).GetOutcome();
    }

    public bool CheckForDatabaseDataPresence()
    {
        using var connection = new SqlConnection(DatabaseConfigurator.GetDatabaseURI());
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "select count(*) from Customer";
        var count = (int)command.ExecuteScalar();

        return count > 0;
    }
}