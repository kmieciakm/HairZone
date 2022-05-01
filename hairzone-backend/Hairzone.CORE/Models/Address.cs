namespace Hairzone.CORE.Models;

public class Address
{
    public Address(string city, string street, string number, string postalCode)
    {
        City = city;
        Street = street;
        Number = number;
        PostalCode = postalCode;
    }

    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string PostalCode { get; set; }
}