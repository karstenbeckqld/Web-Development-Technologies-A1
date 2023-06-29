using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MyBankDbAccess.Extensions;
using MyBankDbAccess.Models;

namespace MyBankDbAccess.Core;

// The Database class serves as a generic interface for the model to the database. It forms par of our Object-Relational
// Mapping and returns required data from the database to the view. 
public class Database<T>
{
    private readonly string _connectionString = DatabaseConfigurator.GetDatabaseURI();
    private string Query { get; set; }
    private Dictionary<string, object> SqlParameters { get; set; }

    private readonly Dictionary<string, string> _where;

    private readonly Type _objectType;

    private readonly string _tableName;

    private int _externalCustomerId;

    public Database()
    {
        _where = new Dictionary<string, string>();
        SqlParameters = new Dictionary<string, object>();
        _objectType = typeof(T);
        _tableName = _objectType.Name;
        _externalCustomerId = 0;
    }

    // The Where() method serves as a selector for specific attributes, like the WHERE clause in SQL. It returns a
    // Database<T> object, so that it can get chained with other methods of this class. 
    public Database<T> Where(string key, string value)
    {
        _where.Add(key, value);
        return this;
    }

    // The SetCustomerId() method is only required for the Web Service as we need to set the customer Id value for the
    // Login type from the Customer type.
    public Database<T> SetCustomerId(int value)
    {
        _externalCustomerId = value;
        return this;
    }

    // The CheckForDatabaseDataPresence() method serves only as a checkpoint in the beginning to see if data is present
    // in the database, so tha the application can decide if he data must be loaded from the web service. 
    public bool CheckForDatabaseDataPresence()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "select count(*) from Customer";
        var count = (int)command.ExecuteScalar();

        return count > 0;
    }

    // From here on, we have a set of methods that all return a Database<T> object, so that they can get chained. The
    // GetAll() method produces a query string that fetches all records of a given table.
    public Database<T> GetAll()
    {
        Query = "SELECT * FROM [" + _tableName + "] ";

        PropertyInfo[] properties;

        if (_objectType == typeof(Customer) || _objectType == typeof(Account))
        {
            properties = _objectType.GetFilteredProperties();
        }
        else
        {
            properties = _objectType.GetProperties();
        }

        foreach (var property in properties)
        {
            if (property != null)
            {
                SqlParameters.Add(property.Name, property.Name);
            }
        }

        return this;
    }

    // The Insert() method produces a query string that inserts a dataset into the corresponding table of the given
    // model type. 
    public Database<T> Insert(T model)
    {
        var t = typeof(T);

        var table = t.Name;

        var query = "INSERT INTO [" + _tableName + "] (";

        var values = " VALUES (";

        var properties = t.GetFilteredProperties();

        foreach (var property in properties)
        {
            query += property.Name + ",";

            values += "@" + property.Name + ",";

            SqlParameters.Add(property.Name, property.GetValue(model));
        }

        if (_externalCustomerId > 0)
        {
            SqlParameters["CustomerID"] = _externalCustomerId;
        }

        query = query.TrimEnd(',') + ")";
        values = values.TrimEnd(',') + ")";

        Query = query + values;


        Console.WriteLine(Query);
        foreach (var parameter in SqlParameters)
        {
            Console.WriteLine(parameter.Key + ": " + parameter.Value);
        }

        return this;
    }

    // The Update() method produces a query string that updates a dataset in the corresponding table of the given
    // model type. 
    public Database<T> Update(T model)
    {
        var t = typeof(T);
        var table = t.Name;
        var query = "UPDATE [" + table + "] SET ";

        var pInfo = t.GetFilteredProperties();

        foreach (var p in pInfo)
        {
            query += p.Name + "=" + "@" + p.Name + ",";
            SqlParameters.Add("@" + p.Name, p.GetValue(model) ?? DBNull.Value);
        }

        query = query.TrimEnd(',') + " WHERE " + pInfo[0].Name + "=" + "@" + pInfo[0].Name;

        Query = query;

        return this;
    }

    // As we deal with two types of database actions, getting and setting, we have the Execute() method which will
    // perform any insert and update transactions, using the built Query and SqlParameters.
    public int Execute()
    {
        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText = Query;

        foreach (var parameter in SqlParameters)
        {
            command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
        }

        var updates = command.ExecuteNonQuery();

        return updates;
    }

    // To fetch data from the database we use the GetAll() method which will use the Query value to create a command and
    // the CreateDataTable class to build the result set.
    public List<T> GetResult()
    {
        using var connection = new SqlConnection(_connectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        if (!_where.IsNullOrEmpty())
        {
            Query += " WHERE " + _where.FirstOrDefault().Key + " = @" + _where.FirstOrDefault().Key;
        }

        command.CommandText = Query;

        if (!_where.IsNullOrEmpty())
        {
            command.Parameters.AddWithValue(_where.FirstOrDefault().Key, _where.FirstOrDefault().Value);
        }
        else
        {
            foreach (var parameter in SqlParameters.Where(parameter => parameter.Value != null))
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
        }

        return CreateDataTable.ReturnRows<T>(command, _objectType);
    }
}