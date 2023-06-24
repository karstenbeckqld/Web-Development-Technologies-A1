using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace A1ClassLibrary.Utils;

// The below code has been provided by Dr. Matthew Bolger during week 3 lecture and tutorial and gets used in the
// CustomerManager, AccountManager, LoginManager and TransactionManager classes.
public static class Utilities
{
    public static bool IsInRange(this int value, int min, int max) => value >= min && value <= max;

    public static DataTable GetDataTable(this SqlCommand command)
    {
        using var adapter = new SqlDataAdapter(command);

        var table = new DataTable();
        adapter.Fill(table);

        return table;
    }

    public static object GetObjectOrDbNull(this object value) => value ?? DBNull.Value;
}