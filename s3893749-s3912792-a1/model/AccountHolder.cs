using System.Diagnostics.CodeAnalysis;

namespace s3893749_s3912792_a1.model;

public class AccountHolder
{
    private List<Account> _accounts;
    
    [SetsRequiredMembers]
    public AccountHolder(int customerId, char name, char address, char city, int postcode)
    {
        CustomerId = customerId;
        Name = name;
        Address = address;
        City = city;
        Postcode = postcode;
        _accounts = new List<Account>(2);
    }

    public char Name { get; set; }

    public char Address { get; set; }

    public char City { get; set; }

    public int Postcode { get; set; }

    public int CustomerId { get; set; }

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public List<Account> GetAccounts()
    {
        return _accounts;
    }
}