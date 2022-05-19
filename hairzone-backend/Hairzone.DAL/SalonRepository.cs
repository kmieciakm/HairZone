using Hairzone.CORE.Infrastructure;
using Hairzone.CORE.Models;
using Hairzone.DAL.Tables;

namespace Hairzone.DAL;

public class SalonRepository : ISalonRepository
{
    private IAddressTable _addressTable { get; }
    private ISalonTable _salonTable { get; }

    public SalonRepository(IAddressTable addressTable, ISalonTable salonTable)
    {
        _addressTable = addressTable;
        _salonTable = salonTable;
    }

    public async Task CreateSalonAsync(Guid id, Salon salon)
    {
        try
        {
            AddressEntity address = AddressEntity.Create(salon.Address);
            _addressTable.Insert(address);

            SalonEntity salonEntity = SalonEntity.Create(salon, address.Id);
            _salonTable.Insert(salonEntity);

            await _addressTable.CommitAsync();
            await _salonTable.CommitAsync();
        }
        catch (Exception)
        {
            _addressTable.Rollback();
            _salonTable.Rollback();
            throw;
        }
    }

    public async Task<IEnumerable<Salon>> GetAllByCity(string city)
    {
        var addresses = await _addressTable
            .QueryAsync(add => add.City == city);

        List<SalonEntity> salons = new();
        foreach(var address in addresses)
        {
            var salon = (await _salonTable
                .QueryAsync(salon => salon.AddressId == address.Id))
                .FirstOrDefault();
            if (salon is not null)
            {
                salons.Add(salon);
            }
        }

        return salons.Select(
            salon => SalonEntity.ToDomain(salon, addresses.First(add => add.Id.Equals(salon.AddressId))));
    }

    public async Task<IEnumerable<string>> GetCities()
    {
        var addresses = await _addressTable
            .QueryAsync(add => add.City != string.Empty);

        return addresses
            .Select(add => add.City)
            .Distinct();
    }
}