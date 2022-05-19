using Hairzone.CORE.Models;

namespace Hairzone.CORE.Contracts;

public interface ISalonAdministrationService
{
    Task<IEnumerable<string>> GetCities();
    Task RegisterSalonAsync(Salon salon);
    Task<IEnumerable<Salon>> GetAvailableSalonsInCity(string city);
}
