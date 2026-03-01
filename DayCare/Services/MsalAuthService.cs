using DayCare.Models;
using Microsoft.Identity.Client;

namespace DayCare.Services;

public class MsalAuthService : IAuthService
{
    // Replace these with your actual Azure AD / Microsoft Entra app registration values.
    // To register your app:
    // 1. Go to https://portal.azure.com/ and navigate to Azure Active Directory > App registrations
    // 2. Create a new registration and copy the Application (client) ID and Directory (tenant) ID
    // 3. Under "Authentication", add a platform for "Mobile and desktop applications"
    //    and register the redirect URI (e.g., msal{ClientId}://auth)
    private const string ClientId = "YOUR_CLIENT_ID";
    private const string TenantId = "YOUR_TENANT_ID";
    private const string Authority = $"https://login.microsoftonline.com/{TenantId}";

    private static readonly string[] Scopes = new[]
    {
        "openid",
        "profile",
        "email",
        "User.Read"
    };

    private readonly IPublicClientApplication _msalClient;
    private User? _currentUser;

    public User? CurrentUser => _currentUser;

    public MsalAuthService()
    {
        _msalClient = PublicClientApplicationBuilder
            .Create(ClientId)
            .WithAuthority(Authority)
            .WithRedirectUri(GetRedirectUri())
            .Build();
    }

    public async Task<User?> SignInAsync()
    {
        AuthenticationResult? result = null;

        try
        {
            // Try silent authentication first (cached token)
            var accounts = await _msalClient.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();

            if (firstAccount != null)
            {
                result = await _msalClient
                    .AcquireTokenSilent(Scopes, firstAccount)
                    .ExecuteAsync();
            }
        }
        catch (MsalUiRequiredException)
        {
            // Silent auth failed — fall through to interactive
        }

        if (result == null)
        {
            try
            {
                result = await _msalClient
                    .AcquireTokenInteractive(Scopes)
                    .ExecuteAsync();
            }
            catch (MsalException ex)
            {
                System.Diagnostics.Debug.WriteLine($"MSAL error: {ex.Message}");
                return null;
            }
        }

        if (result == null) return null;

        _currentUser = new User
        {
            Id = result.Account.HomeAccountId.Identifier,
            DisplayName = result.Account.Username,
            Email = result.Account.Username,
            AccessToken = result.AccessToken
        };

        return _currentUser;
    }

    public async Task SignOutAsync()
    {
        var accounts = await _msalClient.GetAccountsAsync();
        foreach (var account in accounts)
        {
            await _msalClient.RemoveAsync(account);
        }
        _currentUser = null;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var accounts = await _msalClient.GetAccountsAsync();
        if (!accounts.Any()) return false;

        try
        {
            var result = await _msalClient
                .AcquireTokenSilent(Scopes, accounts.First())
                .ExecuteAsync();
            return result != null;
        }
        catch
        {
            return false;
        }
    }

    private static string GetRedirectUri()
    {
#if ANDROID
        return $"msal{ClientId}://auth";
#elif IOS || MACCATALYST
        return $"msal{ClientId}://auth";
#elif WINDOWS
        return "https://login.microsoftonline.com/common/oauth2/nativeclient";
#else
        return "https://login.microsoftonline.com/common/oauth2/nativeclient";
#endif
    }
}
