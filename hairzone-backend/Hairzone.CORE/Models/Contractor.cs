namespace Hairzone.CORE.Models;

public class Contractor
{
    public Contractor(string name, string phone, string email, Address address)
        : this(Guid.NewGuid(), name, phone, email, address)
    {
    }

    public Contractor(Guid id, string name, string phone, string email, Address address)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
    }

    public Guid Id { get; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public Address Address { get; }
}
