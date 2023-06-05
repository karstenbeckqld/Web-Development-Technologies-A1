using System.Diagnostics.CodeAnalysis;

namespace s3893749_s3912792_a1.model;

public class AccountHolder
{
    public char Name { get; set; }

    public char Address { get; set; }

    public char City { get; set; }

    public int Postcode { get; set; }

    [SetsRequiredMembers]
    public AccountHolder(char name, char address, char city, int postcode)
    {
        Name = name;
        Address = address;
        City = city;
        Postcode = postcode;
    }
}