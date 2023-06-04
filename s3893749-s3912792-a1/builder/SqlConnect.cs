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
            builder.DataSource = "<your_server>.database.windows.net";
            builder.UserID = "<username>";
            builder.Password = "<password>";
            builder.InitialCatalog = "<your_database>";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("\nQuery data example:");
                Console.WriteLine("==========================================");

                String sql = "SELECT name, collation_name FROM sys.databases";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                        }
                    }
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