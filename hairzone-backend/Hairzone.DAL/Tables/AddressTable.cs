using AzureTables;
using Hairzone.CORE.Models;
using Microsoft.Extensions.Options;

namespace Hairzone.DAL.Tables;

public class AddressEntity : Entity
{
    public AddressEntity() { }

    public AddressEntity(string city, string street, string number, string postalCode)
    {
        Id = Guid.NewGuid();
        City = city;
        Street = street;
        Number = number;
        PostalCode = postalCode;
    }

    public string City { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string PostalCode { get; set; }

    public static AddressEntity Create(Address address) => new(
        address.City,
        address.Street,
        address.Number,
        address.PostalCode
    );

    internal static Address ToDomain(AddressEntity addressEntity) => new(
        addressEntity.City,
        addressEntity.Street,
        addressEntity.Number,
        addressEntity.PostalCode
    );
}

public interface IAddressTable : ITable<AddressEntity, Guid>
{
}


[Table("Address")]
public class AddressTable : Table<AddressEntity, Guid>, IAddressTable
{
    public AddressTable(IOptions<AzureStorageSettings> storageOptions)
        : base(storageOptions.Value)
    {
    }
}
