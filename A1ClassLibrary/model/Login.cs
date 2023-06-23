<<<<<<<< HEAD:s3893749-s3912792-a1/project/model/Login.cs
namespace s3893749_s3912792_a1.project.model;
========
namespace A1ClassLibrary.model;
>>>>>>>> main:A1ClassLibrary/model/Login.cs

/* The Login class stores the Login object for each customer. */
public class Login
{
    
    /*private string _loginId;
    private int _customerId;
    private string _passwordHash;
    

    public Login(string loginId, int customerId, string passwordHash)
    {
        _loginId = loginId;
        _customerId = customerId;
        _passwordHash = passwordHash;
    }*/
    /* The LoginId property stores the login id for a customer. */
    public string LoginID { get; set; }
    
    /* The PasswordHash property stores the password for a customer. */
    public string PasswordHash { get; set; }
    
    /* The CustomerId property stores a customer's ID. */
    public int CustomerID { get; set; }

    public override string ToString()
    {
        return $"CustomerID: {CustomerID}, LoginID: {LoginID}, PasswordHash: {PasswordHash}";
    }
}