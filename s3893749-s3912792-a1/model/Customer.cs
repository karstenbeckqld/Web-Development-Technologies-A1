using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace s3893749_s3912792_a1.model;

public class Customer
{
    private List<Account> _accounts;
    private Login _login;


    [JsonConstructor]
    public Customer(int customerId, string name, string address, string city, string postcode, Login login)
    {
        CustomerId = customerId;
        Name = name;
        Address = address;
        City = city;
        PostCode = postcode;
        _accounts = new List<Account>(2);
        _login = login;
    }

    [SetsRequiredMembers]
    public Customer(int customerId, string name, string address, string city, string postcode)
    {
        CustomerId = customerId;
        Name = name;
        Address = address;
        City = city;
        PostCode = postcode;
        _accounts = new List<Account>(2);
    }
    
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string PostCode { get; set; }

    

    public List<Account> Accounts { get; set; }

    public List<Login> Login { get; set; }
    
    /*public string LoginId { get; set; }
    public string PasswordHash { get; set; }*/


    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public List<Account> GetAccounts()
    {
        return _accounts;
    }

    public void SetLoginData(Login data)
    {
        _login = data;
    }
}