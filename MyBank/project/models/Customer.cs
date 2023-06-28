#nullable enable
using System.Diagnostics.CodeAnalysis;

namespace MyBank.project.models;

// The Customer class represents a user defined type that holds data from the Customer, Account and Login tables. 
public class Customer
{
    public Customer()
    {
    }


    [SetsRequiredMembers]
    public Customer(int customerId, string name, string? address, string? city, string? postCode)
    {
        CustomerID = customerId;
        Name = name;
        Address = address;
        City = city;
        PostCode = postCode;
    }

    
     public required int CustomerID { get; set; }

    public string Name { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? PostCode { get; set; }
    public List<Account> Accounts { get; set; }
    public Login Login { get; set; }


    public override string ToString()
    {
        return $"CustomerID: {CustomerID}, Name: {Name}, Address: {Address}, City: {City}, PostCode: {PostCode}";
    }
}