using System.Diagnostics.CodeAnalysis;
using MyBankDbAccess.attributes;

namespace MyBankDbAccess.Models;

//The Login class stores data from the Login table in the database.
public class Login 
{

    public Login()
    {
    }

    [SetsRequiredMembers]
    public Login(string loginId, string passwordHash, int customerId)
    {
        LoginID = loginId;
        PasswordHash = passwordHash;
        CustomerID = customerId;
    }

    // The LoginId property stores the login id for a customer.
    public required string LoginID { get; init; }

    // The PasswordHash property stores the password for a customer.
    public string PasswordHash { get; set; }

    // The CustomerID property stores a customer's ID.
    public int CustomerID { get; init; }

    public override string ToString()
    {
        return $"CustomerID: {CustomerID}, LoginID: {LoginID}, PasswordHash: {PasswordHash}";
    }
}