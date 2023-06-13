using Newtonsoft.Json;

namespace s3893749_s3912792_a1.model;

public class Login
{

    /*private int _customerId;
    private string _loginId;
    private string _passwordHash;

    public Login(int customerId, string loginId, string passwordHash)
    {
        _customerId = customerId;
        _loginId = loginId;
        _passwordHash = passwordHash;
    }*/
    
    
    public string LoginId { get; set; }
    
    public string PasswordHash { get; set; }
    
    public int CustomerId { get; set; }
    
}