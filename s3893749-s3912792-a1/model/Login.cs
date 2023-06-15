using Newtonsoft.Json;
using s3893749_s3912792_a1.interfaces;

namespace s3893749_s3912792_a1.model;

/* The Login class stores the Login object for each customer. */
public class Login:IDatabaseObject
{
    /* The LoginId property stores the login id for a customer. */
    public string LoginId { get; set; }
    
    /* The PasswordHash property stores the password for a customer. */
    public string PasswordHash { get; set; }
    
    /* The CustomerId property stores a customer's ID. */
    public int CustomerId { get; set; }
}