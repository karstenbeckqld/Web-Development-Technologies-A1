using System.Data;
using System.Reflection;
using A1ClassLibrary.Utils;
using Microsoft.Data.SqlClient;

namespace A1ClassLibrary.DBControllers;

public class Database
{
    private readonly string _connectionString = DbConnectionString.DbConnect;

    private string Query { get; set; }
    private Dictionary<string, string> SqlParameters { get; set; }

    public bool CheckForDatabaseDataPresence()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "select count(*) from Customer";
        var count = (int)command.ExecuteScalar();

        return count > 0;
    }

    public List<T> GetAllEntities<T>()
    {
        var entities = new List<T>();

        // Establish SQL Server connection
        using var connection = new SqlConnection(_connectionString);

        // We set the table name to the name of the type which has been set up to match.
        var tableName = typeof(T).Name;

        // Here we define the command and query and open the connection.
        var query = $"SELECT * FROM {tableName}";
        var command = new SqlCommand(query, connection);
        connection.Open();

        // Now we call a reader to read the data from the database.
        var reader = command.ExecuteReader();


        while (reader.Read())
        {
            // As long as the reader receives data, we create a new instance of an object of type T.
            T obj = Activator.CreateInstance<T>();

            // We save the type of T in the objectType variable.
            var objectType = obj.GetType();

            // Here we apply a filter for properties we don't need and that would make the server crash. This is because,
            // for example, the Customer class contains an Accounts and Login property which are not part of the Customer
            // table in the database. To avoid for these properties to get read, we filter them out beforehand.
            var pInfo = objectType.GetFilteredProperties();

            // Now we simply loop through the data in the database and add it to the object instance.
            foreach (var property in pInfo)
            {
                if (reader[property.Name] != DBNull.Value)
                {
                    property.SetValue(obj, reader[property.Name]);
                }
            }

            // Now we add the object instance to the list.
            entities.Add(obj);
        }

        reader.Close();
        return entities;
    }


    public Database Insert<T>(T model)
    {
        Type t = typeof(T);
        string table = t.Name;
        string query = "INSERT INTO [" +table + "] (";
        string values = " VALUES (";
        Dictionary<string, string> parameters = new Dictionary<string, string>();

        PropertyInfo[] pinfo = t.GetFilteredProperties();

        foreach (var p in pinfo)
        {
            query += p.Name + ",";
            values += "@" + p.Name + ",";
            parameters.Add("@" + p.Name,
                p.GetValue(model) != null ? p.GetValue(model)?.ToString() : DBNull.Value.ToString());
        };

        query = query.TrimEnd(',') + ")";
        values = values.TrimEnd(',') + ")";

        Query = query + values;
        SqlParameters = parameters;


        Console.WriteLine(Query);
        foreach (var parameter in parameters)
        {
            Console.WriteLine(parameter.Key + ": " + parameter.Value);
        }

        return this;
    }

    public Database Update<T>(T model)
    {
        var t = typeof(T);
        string table = t.Name;
        string query = "UPDATE " + table + " SET ";

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        PropertyInfo[] pInfo = t.GetFilteredProperties();

        foreach (var p in pInfo)
        {
            query += p.Name + "=" + "@" + p.Name + ",";
            parameters.Add("@" + p.Name,
                p.GetValue(model) != null ? p.GetValue(model)?.ToString() : DBNull.Value.ToString());
        }

        query = query.TrimEnd(',') + " WHERE " + pInfo[0].Name + "=" + "@" + pInfo[0].Name;

        Query = query;
        SqlParameters = parameters;


        Console.WriteLine(query);
        foreach (var parameter in parameters)
        {
            Console.WriteLine(parameter.Key + ", " + parameter.Value);
        }

        return this;
    }

    public int Execute<T>()
    {
        // Establish SQL Server connection
        using var connection = new SqlConnection(_connectionString);
        connection.Open();


        // Here we define the command and query and open the connection.
        using var command = connection.CreateCommand();

        command.CommandText = Query;
        foreach (var parameter in SqlParameters)
        {
            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
        }

        var updates = command.ExecuteNonQuery();
        return updates;
    }
}