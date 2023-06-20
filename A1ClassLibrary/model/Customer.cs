namespace A1ClassLibrary.model;

public class Customer
{
    public int CustomerID { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string PostCode { get; set; }
    
    public List<Account> Accounts { get; set; }

    public Login Login { get; set; }

    public override string ToString()
    {
        return $"CustomerID: {CustomerID}, Name: {Name}, Address: {Address}, City: {City}, PostCode: {PostCode}";
    }
}