using Hairzone.CORE.Models;

namespace Hairzone.CORE.Infrastructure;

public interface ISalonRepository
{
    Task CreateSalonAsync(Guid id, Salon salon);
}
