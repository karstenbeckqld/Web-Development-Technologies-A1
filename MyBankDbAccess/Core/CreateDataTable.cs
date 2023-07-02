using System.Data;
using Microsoft.Data.SqlClient;
using MyBankDbAccess.Extensions;
using MyBankDbAccess.Models;

namespace MyBankDbAccess.Core;

// The CreateDataTable class' ReturnRows() method receives a command and an object from the Database class and returns a
// list of properties for the frontend to work with. Because we work with four different types, this functionality had
// been taken out of the Database class for it to maintain fully generic. The ReturnRows() method builds a datatable frm
// the database for a given command and type and then creates type objects according to the type passed into the method.
// In the end, it returns this list to the calling class. 
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

                         if (property.Name.Equals("TransactionTimeUtc"))
                         {
                             property.SetValue(obj, row.Field<DateTime>(property.Name));
                         }
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