using Hairzone.CORE.Models;

namespace Hairzone.CORE.Contracts;

public interface ISalonAdministrationService
{
    Task RegisterSalonAsync(Salon salon);
}
