using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Hairzone.CORE.Integration;

public class MicroAuthIdentitySettings
{
    public string Url { get; set; }
    public string Key { get; set; }
}

public class MicroAuthIdentityService : IIdentityService
{
    private const string API_KEY_HEADER_NAME = "API-KEY";
    private static HttpClient _Client { get; } = new HttpClient();
    private MicroAuthIdentitySettings _Settings { get; }

    public MicroAuthIdentityService(IOptions<MicroAuthIdentitySettings> settings)
    {
        _Settings = settings.Value;
    }

    public async Task<Account> RegisterAccountAsync(SignUpRequest request)
    {
        MicroAuthSignUpRequest signUpRequest = new(request);
        string body = JsonSerializer.Serialize(signUpRequest);
        StringContent content = new(body, Encoding.UTF8, "application/json");

        // Add a secret API key header to authorize the application for registration 
        content.Headers.Add(API_KEY_HEADER_NAME, _Settings.Key);

        var response = await _Client.PostAsync($"{_Settings.Url}/api/identity/register", content);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Account account = JsonSerializer.Deserialize<Account>(
            responseBody,
            new JsonSerializerOptions() {
                PropertyNameCaseInsensitive = true
            })!;

        // Sends email to the new contractor allowing them to set its' own password.
        await RequestPasswordReset(new (request.Email));

        return account;
    }

    private async Task RequestPasswordReset(PasswordResetRequest request)
    {
        string body = JsonSerializer.Serialize(request);
        StringContent content = new(body, Encoding.UTF8, "application/json");

        var response = await _Client.PostAsync($"{_Settings.Url}/api/identity/forgotpassword", content);
        response.EnsureSuccessStatusCode();
    }

    private record struct PasswordResetRequest(string Email);

    private record struct MicroAuthSignUpRequest(
        string Firstname,
        string Lastname,
        string Email,
        string Password,
        string ConfirmationPassword)
    {
        public MicroAuthSignUpRequest(SignUpRequest request)
            : this(request.Name, request.Name, request.Email, request.Password, request.Password)
        { }
    };
}
