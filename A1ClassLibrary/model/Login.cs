using A1ClassLibrary.Interfaces;

namespace A1ClassLibrary.model;

//The Login class stores data from the Login table in the database.
public class Login :IModel
{
    public Login()
    {
    }

    public Login(string loginId, string passwordHash, int customerId)
    {
        LoginID = loginId;
        PasswordHash = passwordHash;
        CustomerID = customerId;
    }

    /* The LoginId property stores the login id for a customer. */
    public string LoginID { get; set; }

    /* The PasswordHash property stores the password for a customer. */
    public string PasswordHash { get; set; }

    /* The CustomerID property stores a customer's ID. */
    public int CustomerID { get; set; }

    public override string ToString()
    {
        return $"CustomerID: {CustomerID}, LoginID: {LoginID}, PasswordHash: {PasswordHash}";
    }
}