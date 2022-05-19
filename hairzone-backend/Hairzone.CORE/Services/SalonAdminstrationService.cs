using Hairzone.CORE.Contracts;
using Hairzone.CORE.Helpers;
using Hairzone.CORE.Infrastructure;
using Hairzone.CORE.Integration;
using Hairzone.CORE.Models;

namespace Hairzone.CORE.Services;

public class SalonAdminstrationService : ISalonAdministrationService
{
    private IIdentityService _IdentityService { get; }
    private ISalonRepository _SalonRepository { get; }

    public SalonAdminstrationService(IIdentityService identityService, ISalonRepository salonRepository)
    {
        _IdentityService = identityService;
        _SalonRepository = salonRepository;
    }

    public async Task RegisterSalonAsync(Salon salon)
    {
        Account account = await _IdentityService.RegisterAccountAsync(new SignUpRequest(
            salon.Name,
            salon.Email,
            PasswordHelper.GeneratePassword()
        ));
        await _SalonRepository.CreateSalonAsync(account.Guid, salon);
    }

    public async Task<IEnumerable<Salon>> GetAvailableSalonsInCity(string city)
    {
        return await _SalonRepository.GetAllByCity(city);
    }

    public async Task<IEnumerable<string>> GetCities()
    {
        return await _SalonRepository.GetCities();
    }
}
