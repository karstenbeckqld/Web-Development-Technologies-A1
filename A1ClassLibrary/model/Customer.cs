namespace A1ClassLibrary.model;

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

    public int CustomerID { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string PostCode { get; set; }
    
    internal List<Account> Accounts { get; set; }

    public Login Login { get; set; }

    public override string ToString()
    {
        return $"CustomerID: {CustomerID}, Name: {Name}, Address: {Address}, City: {City}, PostCode: {PostCode}";
    }
}