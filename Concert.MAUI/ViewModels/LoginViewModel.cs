using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Concert.MAUI.Services;

namespace Concert.MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthenticationService? _authService;
        
        [ObservableProperty]
        public partial string Email { get; set; }

        [ObservableProperty]
        public partial string Password { get; set; }

        [ObservableProperty]
        public partial string ErrorMessage { get; set; }

        [ObservableProperty]
        public partial bool IsErrorVisible { get; set; }

        [ObservableProperty]
        public partial bool IsLoading { get; set; }

        // Standardkonstruktor för XAML
        public LoginViewModel()
        {
            Email = string.Empty;
            Password = string.Empty;
            ErrorMessage = string.Empty;
            LoginCommand = new AsyncRelayCommand(LoginAsync);
            SignUpCommand = new AsyncRelayCommand(SignUpAsync);
        }

        public LoginViewModel(IAuthenticationService authService)
        {
            _authService = authService;
            Email = string.Empty;
            Password = string.Empty;
            ErrorMessage = string.Empty;
            LoginCommand = new AsyncRelayCommand(LoginAsync);
            SignUpCommand = new AsyncRelayCommand(SignUpAsync);
        }

        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        private async Task LoginAsync()
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;
                IsErrorVisible = false;

                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Please enter your email and password";
                    IsErrorVisible = true;
                    return;
                }

                if (_authService == null)
                {
                    ErrorMessage = "Service not available";
                    IsErrorVisible = true;
                    return;
                }

                var success = await _authService.LoginAsync(Email, Password);
                if (!success)
                {
                    ErrorMessage = "Invalid email or password";
                    IsErrorVisible = true;
                    return;
                }

                // Clear form
                Email = string.Empty;
                Password = string.Empty;
                IsErrorVisible = false;

                // Navigation will be handled by AuthenticationService event
            }
            catch (Exception)
            {
                ErrorMessage = "An error occurred during login";
                IsErrorVisible = true;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task SignUpAsync()
        {
            await Shell.Current.GoToAsync("//signup");
        }
    }
}