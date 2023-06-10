using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace s3893749_s3912792_a1.model;

public class AccountHolder
{
    private List<Account> _accounts;
    private Login _login;

    [SetsRequiredMembers]
    public AccountHolder(int customerId, string name, string address, string city, string postcode, Login login)
    {
        CustomerId = customerId;
        Name = name;
        Address = address;
        City = city;
        Postcode = postcode;
        _accounts = new List<Account>(2);
        _login = login;
    }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Postcode { get; set; }

    public int CustomerId { get; set; }
    
    public List<Account> Accounts { get; set; }

    public Login Login
    {
        get => _login;
        set
        {
            var _login = new Login();
            _login = value;
        }
    }
    
    

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public List<Account> GetAccounts()
    {
        return _accounts;
    }
}