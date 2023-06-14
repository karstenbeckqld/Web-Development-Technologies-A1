namespace s3893749_s3912792_a1.interfaces;

public interface IAccount
{
    public int AccountNumber { get; set; }
    public string AccountType { get; set; }
    public int CustomerID { get; set; }
    public decimal Balance { get; set; }
}