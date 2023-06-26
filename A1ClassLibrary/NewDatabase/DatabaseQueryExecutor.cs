using System.Data;
using EasyDB;
using Microsoft.Data.SqlClient;

namespace A1ClassLibrary.NewDatabase;

public static class DatabaseQueryExecutor
{
    public static DatabaseResponse Query(string query, Dictionary<string, string> parameters)
    {
        DatabaseResponse databaseResponse = new DatabaseResponse();

        string[] querySpit = query.Split(" ");

        if (query.Contains("Withdraw"))
        {
            query = query.Replace("Withdraw", "[Withdraw]");
        }
        
        try
        {
            using (SqlConnection connection =
                   new SqlConnection(DatabaseConfigurator.GetDatabaseURI()))
            {

                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;

                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }

                connection.Open();

                switch (querySpit[0])
                {
                    case "SELECT":
                        
                        databaseResponse.SetExecutionType("SELECT");
                        databaseResponse = Select(command, databaseResponse);
                        break;
                    case "INSERT":
                        databaseResponse.SetExecutionType("INSERT");
                        databaseResponse.SetOutcome(command.ExecuteNonQuery() > 0);
                        break;

                    case "UPDATE":
                        databaseResponse.SetExecutionType("UPDATE");
                        databaseResponse.SetOutcome(command.ExecuteNonQuery() > 0);
                        break;

                }

                connection.Close();

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }

        return databaseResponse;
    }

    private static DatabaseResponse Select(SqlCommand command, DatabaseResponse output)
    {

        DataTable table = new DataTable();
        new SqlDataAdapter(command).Fill(table);

        foreach (DataRow tableRow in table.Rows)
        {
            Dictionary<string, object> row = new Dictionary<string, object>();

            foreach (DataColumn tableColumn in table.Columns)
            {
               
                if (!row.ContainsKey(tableColumn.ToString()))
                {
                    row.Add(tableColumn.ToString(), tableRow.Field<object>(tableColumn.ToString()));
                }
            }
            
            output.AddRowEntry(row);
        }
        
        return output;
    }
}