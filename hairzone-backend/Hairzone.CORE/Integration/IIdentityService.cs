namespace Hairzone.CORE.Integration;

public interface IIdentityService
{
    Task<Account> RegisterAccountAsync(SignUpRequest request);
}

public record struct SignUpRequest(
    string Name,
    string Email,
    string Password
);

public class Account
{
    public Guid Guid { get; set; }
};
