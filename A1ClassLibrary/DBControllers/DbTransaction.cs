using A1ClassLibrary.Utils;
using Microsoft.Data.SqlClient;

namespace A1ClassLibrary.DBControllers;

public class DbTransaction
{

    
    private Dictionary<Type, string> _data;
    public DbTransaction(Dictionary<Type, string> transactions)
    {
        _data = transactions;
    }

    public void PerformTransaction()
    {
        using var connection = new SqlConnection(DbConnectionString.DbConnect);

        using var transaction = connection.BeginTransaction();

        var command = connection.CreateCommand();

        foreach (var keyValuePair in _data)
        {
            command.CommandText = keyValuePair.Value;

            Console.WriteLine(command.CommandText);

            var properties = keyValuePair.Key.GetProperties();

            foreach (var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(property));

                Console.WriteLine(property.Name, property.GetValue(property));
            }

            command.Transaction = transaction;
        }

        transaction.Commit();
    }
}