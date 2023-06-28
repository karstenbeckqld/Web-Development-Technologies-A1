using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MyBankDbAccess.Extensions;
using MyBankDbAccess.Models;
using MyBankDatabase.Utils;

namespace MyBankDbAccess.Core;

public class Database<T>
{
    private readonly string _connectionString = DbConnectionString.DbConnect;

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

    public Database<T> Where(string key, string value)
    {
        _where.Add(key, value);
        return this;
    }

    public Database<T> SetCustomerId(int value)
    {
        _externalCustomerId = value;
        return this;
    }

    public bool CheckForDatabaseDataPresence()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "select count(*) from Customer";
        var count = (int)command.ExecuteScalar();

        return count > 0;
    }

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