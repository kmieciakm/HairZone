using Hairzone.CORE.Models;

namespace Hairzone.CORE.Infrastructure;

public interface ISalonRepository
{
    Task<IEnumerable<string>> GetCities();
    Task CreateSalonAsync(Guid id, Salon salon);
    Task<IEnumerable<Salon>> GetAllByCity(string city);
}
