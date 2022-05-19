using AzureTables;
using Hairzone.CORE.Models;
using Microsoft.Extensions.Options;

namespace Hairzone.DAL.Tables;

public class SalonEntity : Entity
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public TimeOnly? OpeningTime { get; set; }
    public TimeOnly? ClosingTime { get; set; }
    public Guid AddressId { get; set; }

    public SalonEntity() { }

    public SalonEntity(Guid guid, string name, string phone, string email, TimeOnly? openingTime, TimeOnly? closingTime, Guid addressId)
    {
        Id = guid;
        Name = name;
        Phone = phone;
        Email = email;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        AddressId = addressId;
    }

    public static SalonEntity Create(Salon salon, Guid addressId) => new(
        salon.Guid,
        salon.Name,
        salon.Phone,
        salon.Email,
        salon.OpeningTime,
        salon.ClosingTime,
        addressId
    );

    internal static Salon ToDomain(SalonEntity salonEntity, AddressEntity addressEntity) => new(
        salonEntity.Id,
        salonEntity.Name,
        salonEntity.Phone,
        salonEntity.Email,
        AddressEntity.ToDomain(addressEntity)
    );
}

public interface ISalonTable : ITable<SalonEntity, Guid>
{
}


[Table("Salon")]
public class SalonTable : Table<SalonEntity, Guid>, ISalonTable
{
    public SalonTable(IOptions<AzureStorageSettings> storageOptions)
        : base(storageOptions.Value)
    {
    }
}
