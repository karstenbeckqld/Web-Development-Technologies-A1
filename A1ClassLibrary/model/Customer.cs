#nullable enable
using System.Diagnostics.CodeAnalysis;
using A1ClassLibrary.DBControllers;
using A1ClassLibrary.Utils;

namespace A1ClassLibrary.model;

public class Customer
{
    public required int CustomerID { get; set; }

    public string Name { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? PostCode { get; set; }
    [SkipProperty] public List<Account> Accounts { get; set; }
    [SkipProperty] public Login Login { get; set; }

    /*public List<string> Blacklist
    {
        get => throw new NotImplementedException();
        set
        {
            Blacklist.Add("Accounts");
            Blacklist.Add("Login");
        }
    }*/

    public override string ToString()
    {
        return $"CustomerID: {CustomerID}, Name: {Name}, Address: {Address}, City: {City}, PostCode: {PostCode}";
    }
}