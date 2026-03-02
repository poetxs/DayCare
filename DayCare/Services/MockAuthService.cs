using DayCare.Models;

namespace DayCare.Services;

public class MockAuthService : IAuthService
{
    private User? _currentUser;

    public User? CurrentUser => _currentUser;

    public Task<User?> SignInAsync()
    {
        _currentUser = new User
        {
            Id = "mock-user-id",
            DisplayName = "Test User",
            Email = "testuser@example.com",
            AccessToken = "mock-access-token"
        };

        return Task.FromResult<User?>(_currentUser);
    }

    public Task SignOutAsync()
    {
        _currentUser = null;
        return Task.CompletedTask;
    }

    public Task<bool> IsAuthenticatedAsync()
    {
        return Task.FromResult(_currentUser is not null);
    }
}
