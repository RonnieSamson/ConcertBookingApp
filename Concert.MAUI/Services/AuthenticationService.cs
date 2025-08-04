using System;
using System.Threading.Tasks;

namespace Concert.MAUI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private string? _currentUserId;
        private string? _currentUserName;
        private string? _currentUserEmail;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(_currentUserId);

        public string? CurrentUserId => _currentUserId;

        public string? CurrentUserName => _currentUserName;

        public event EventHandler<AuthenticationChangedEventArgs>? AuthenticationChanged;

        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                    return false;

                var user = await _userService.GetUserByEmailAsync(email);
                if (user == null || user.Password != password)
                    return false;

                SetAuthenticatedUser(user.Id, user.Name, user.Email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task LogoutAsync()
        {
            _currentUserId = null;
            _currentUserName = null;
            _currentUserEmail = null;

            AuthenticationChanged?.Invoke(this, new AuthenticationChangedEventArgs
            {
                IsAuthenticated = false,
                UserId = null,
                UserName = null
            });

            return Task.CompletedTask;
        }

        public void SetAuthenticatedUser(string userId, string userName, string email)
        {
            _currentUserId = userId;
            _currentUserName = userName;
            _currentUserEmail = email;

            AuthenticationChanged?.Invoke(this, new AuthenticationChangedEventArgs
            {
                IsAuthenticated = true,
                UserId = userId,
                UserName = userName
            });
        }
    }
}
