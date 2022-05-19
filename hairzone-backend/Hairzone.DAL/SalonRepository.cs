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
}