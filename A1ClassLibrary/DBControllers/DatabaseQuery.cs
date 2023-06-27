using System.ComponentModel.Design;
using System.Data;
using A1ClassLibrary.model;
using A1ClassLibrary.Utils;
using Microsoft.Data.SqlClient;

namespace A1ClassLibrary.DBControllers;

public class DatabaseQuery<T>
{
    public static DatabaseResponse<T> Query(string query, Dictionary<string, string> parameters)
    {
        var databaseResponse = new DatabaseResponse<T>();

        var splitQuery = query.Split(" ");
        
        using var connection = new SqlConnection(DbConnectionString.DbConnect);
        var command = connection.CreateCommand();
        command.CommandText = query;

        foreach (var parameter in parameters)
        {
            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
        }
        
        connection.Open();

        switch (splitQuery[0])
        {
            case "SELECT":
                databaseResponse.SetExecutionType("SELECT");
                databaseResponse = Select(command, databaseResponse);
                break;
            case "INSERT":
                databaseResponse.SetExecutionType("INSERT");
                break;
            case "UPDATE":
                databaseResponse.SetExecutionType("UPDATE");
                break;
        }
        
        connection.Close();

        return databaseResponse;
    }
    
    private static DatabaseResponse<T> Select(SqlCommand command, DatabaseResponse<T> output)
    {

        var objectType = typeof(T);
        
        using var adapter = new SqlDataAdapter(command);

        var table = new DataTable();
        adapter.Fill(table);

        foreach (var row in table.Select())
        {
            var obj = Activator.CreateInstance<T>();
            
            if (objectType == typeof(Customer) || objectType == typeof(Account))
            {
                foreach (var property in obj.GetType().GetFilteredProperties())
                {
                    property.SetValue(obj, row.Field<object>(property.Name));
                }
            }
            else
            {
                foreach (var property in obj.GetType().GetProperties())
                {
                    property.SetValue(obj, row.Field<object>(property.Name));
                }
            }
            
            output.AddRowEntry(obj);
        }
        
        return output;
    }
}