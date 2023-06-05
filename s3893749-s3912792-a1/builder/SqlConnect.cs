using Microsoft.Data.SqlClient;

namespace s3893749_s3912792_a1.builder;

public class SqlConnect
{
    public SqlConnect()
    {
        
    }

    public void Connect()
    {
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
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
                            Console.WriteLine("{0} {1}", reader.GetChar(1), reader.GetDateTime(2));
                        }
                    }
                
            
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }

        Console.ReadLine();
    }
}