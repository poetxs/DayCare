using DayCare.Models;

namespace DayCare.Services;

public interface IAuthService
{
    Task<User?> SignInAsync();
    Task SignOutAsync();
    Task<bool> IsAuthenticatedAsync();
    User? CurrentUser { get; }
}
