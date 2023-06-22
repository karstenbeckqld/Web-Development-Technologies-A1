using System.Data;
using A1ClassLibrary.Utils;
using Microsoft.Data.SqlClient;

namespace A1ClassLibrary.DBControllers;

public class DbQueryController
{
    //private static readonly string _connectionString = DbConnectionString.DbConnect;
    private IDbConnection _connection = null;
    private IDbCommand _cmd = null;

    public DbQueryController(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        _cmd = _connection.CreateCommand();
    }

    public DbQueryController AddParameter<T>(string name, T value)
    {
        SqlParameter param = new SqlParameter();
        param.ParameterName = name;
        param.Value = value;
        _cmd.Parameters.Add(param);
        return this;
    }

    public int ExecuteNonQuery(string query)
    {
        var numOfAffectedRows = 0;

        using (_connection)
        {
            _cmd.CommandText = query;
            _connection.Open();
            numOfAffectedRows = _cmd.ExecuteNonQuery();
        }
        
        return numOfAffectedRows;
    }

    public IEnumerable<T> ExecuteQuery<T>(string query)
    {
        IList<T> list = new List<T>();
        Type t = typeof(T);

        using (_connection)
        {
            _cmd.CommandText = query;
            _connection.Open();

            var reader = _cmd.ExecuteReader();
            while (reader.Read())
            {
                T obj = (T)Activator.CreateInstance(t);
                t.GetProperties().ToList().ForEach(p =>
                {
                    if (!p.Name.Equals("Accounts") && !p.Name.Equals("Login")){
                        p.SetValue(obj, reader[p.Name]);
                    }
                });
                
                list.Add(obj);
            }
        }

        return list;
    }
}