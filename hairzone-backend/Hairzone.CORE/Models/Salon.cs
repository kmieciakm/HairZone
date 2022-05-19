namespace Hairzone.CORE.Models;

public class Salon
{
    public Salon(string name, string phone, string email, Address address)
    {
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
    }

    public Salon(Guid guid, string name, string phone, string email, Address address)
    {
        Guid = guid;
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
    }

    public Salon(Contractor contractor)
    : this(contractor.Name, contractor.Phone, contractor.Email, contractor.Address)
    { }

    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public TimeOnly? OpeningTime { get; set; }
    public TimeOnly? ClosingTime { get; set; }
    public Address Address { get; }
}
