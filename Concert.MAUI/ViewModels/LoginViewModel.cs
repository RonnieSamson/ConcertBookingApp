using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Concert.MAUI.Services;


namespace Concert.MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IUserService _userService;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private string errorMessage;
        [ObservableProperty]
        private bool isErrorVisible;

        public LoginViewModel() { }

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new AsyncRelayCommand(LoginAsync);
            SignUpCommand = new AsyncRelayCommand(SignUpAsync);
        }
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter your email and password";
                IsErrorVisible = true;
                return;
            }
            var user = await _userService.GetUserByEmailAsync(Email);
            if (user == null || user.Password != Password)
            {
                ErrorMessage = "Invalid email or password";
                IsErrorVisible = true;
                return;
            }
            IsErrorVisible = false;
            await Shell.Current.GoToAsync("//Homepage");

            


            // Navigate to the home page
        }
        private async Task SignUpAsync()
        {
            await Shell.Current.GoToAsync("//signup");
        }

    }
}
