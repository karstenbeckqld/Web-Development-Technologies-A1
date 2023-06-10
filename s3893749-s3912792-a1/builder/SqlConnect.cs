using System.Data;
using Microsoft.Data.SqlClient;

namespace s3893749_s3912792_a1.builder;

public class SqlConnect
{

    const string connectionString =
        "server=rmit.australiaeast.cloudapp.azure.com;Encrypt=False;uid=s3912792_a1;pwd=abc123";

    public SqlConnect()
    {
        DisconnectedAccess(connectionString);
    }

    private static void DisconnectedAccess(string connectionString)
    {
        // NOTE: Can use a using declaration instead of a using block.
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "select * from log";

        // DataTable stores all rows
        var table = new DataTable();
        new SqlDataAdapter(command).Fill(table);

        foreach (var row in table.Select())
        {
            Console.WriteLine($"{row["ip"]}\n{row["time"]}\n{row["request"]}\n{row["status"]}\n{row["size"]}\n");
        }
    }
    
    
    /*public static void Connect()
    {
        
        try
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "rmit.australiaeast.cloudapp.azure.com";
            builder.UserID = "s3912792_a1";
            builder.Password = "abc123";
            builder.InitialCatalog = "s3912792_a1";
            builder.TrustServerCertificate = true;

            Console.WriteLine(builder.ConnectionString);

            using var connection = new SqlConnection(builder.ConnectionString);

            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("==========================================");

            String sql = "SELECT * FROM log";

            using var command = new SqlCommand(sql, connection);

            connection.Open();
            using var reader = command.ExecuteReader();
            {
                while (reader.Read())
                {
                    Console.WriteLine("Index: {0} - Date: {1}", reader.GetInt32(0), reader.GetDateTime(2));
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }

        Console.ReadLine();
    }*/
    
    
}