using System.Data;
using System.Reflection;
using A1ClassLibrary.Utils;
using Microsoft.Data.SqlClient;

namespace A1ClassLibrary.DBControllers;

public class Database
{
    private readonly string _connectionString = DbConnectionString.DbConnect;


    public void NewSave<T>(T model)
    {
        Type t = typeof(T);
        string table = t.Name;
        string query = "INSERT INTO " + table + " (";
        string values = " VALUES (";
        Dictionary<string, string> parameters = new Dictionary<string, string>();

        t.GetProperties().ToList().ForEach(p =>
        {
            query += p.Name + ",";
            values += "@" + p.Name + ",";
            parameters.Add("@" + p.Name, p.GetValue(model)?.ToString());
        });

        query = query.TrimEnd(',') + ")";
        values = values.TrimEnd(',') + ")";

        var combined = query + values;
        Console.WriteLine(combined);
        foreach (var parameter in parameters)
        {
            Console.WriteLine(parameter.Key + ": " + parameter.Value);
        }
    }

    public List<T> GetAllEntities<T>()
    {
        var entities = new List<T>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        var tableName = typeof(T).Name;
        var query = $"SELECT * FROM {tableName}";
        var command = new SqlCommand(query, connection);
        connection.Open();
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            T obj = Activator.CreateInstance<T>();

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (reader[property.Name] != DBNull.Value)
                {
                    property.SetValue(obj, reader[property.Name]);
                }
            }

            entities.Add(obj);
        }

        reader.Close();
        return entities;
    }
}