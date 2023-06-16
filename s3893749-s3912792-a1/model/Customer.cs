using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using A1ClassLibrary;
using s3893749_s3912792_a1.interfaces;

namespace s3893749_s3912792_a1.model;

public class Customer
{

    /*private int _customerId;
    private string _name;
    private string _address;
    private string _city;
    private string _postCode;
    public Customer(int customerId, string customerName, string customerAddress, string customerCity,
        string customerPostCode)
    {
        _customerId = customerId;
        _name = customerName;
        _address = customerAddress;
        _city = customerCity;
        _postCode = customerPostCode;
    }*/

    public int CustomerId { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string PostCode { get; set; }
    
    public List<Account> Accounts { get; set; }

    public Login Login { get; set; }
}