using System.Data;
using Microsoft.Data.SqlClient;
using MyBankDbAccess.Extensions;
using MyBankDbAccess.Models;

namespace MyBankDbAccess.Core;

public static class CreateDataTable
{
    public static List<T> ReturnRows<T>(SqlCommand command, Type objectType)
    {
        var list = new List<T>();
        
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
            else if (objectType == typeof(Transaction))
            {
                foreach (var property in obj.GetType().GetProperties())
                {
                    if (!property.Name.Equals("CustomerID"))
                    {
                         property.SetValue(obj, row.Field<object>(property.Name));
                    }
                   
                }
            }
            else
            {
                foreach (var property in obj.GetType().GetProperties())
                {
                    property.SetValue(obj, row.Field<object>(property.Name));
                }
            }
            
            list.Add(obj);
        }

        return list;
    }
}