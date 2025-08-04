using System;

namespace Concert.MAUI.Services
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }
        string? CurrentUserId { get; }
        string? CurrentUserName { get; }
        
        event EventHandler<AuthenticationChangedEventArgs> AuthenticationChanged;
        
        Task<bool> LoginAsync(string email, string password);
        Task LogoutAsync();
        void SetAuthenticatedUser(string userId, string userName, string email);
    }

    public class AuthenticationChangedEventArgs : EventArgs
    {
        public bool IsAuthenticated { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
    }
}
