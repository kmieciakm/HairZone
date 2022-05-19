namespace Hairzone.CORE.Models;

public class Contractor
{
    public Contractor(string name, string phone, string email, Address address)
    {
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
    }

    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public Address Address { get; }
}
