using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Concert.MAUI.Models;
using Concert.MAUI.Services;

namespace Concert.MAUI.ViewModels
{
    [QueryProperty(nameof(UserIdQuery), nameof(UserIdQuery))]
    [QueryProperty(nameof(PerformanceIdsQuery), nameof(PerformanceIdsQuery))]
    public partial class BookingPageViewModel : ObservableObject
    {
        private readonly IUserService _userService;
        private readonly IPerformanceService _performanceService;
        private readonly IBookingService _bookingService;

        [ObservableProperty]
        private string? userIdQuery;

        [ObservableProperty]
        private string? performanceIdsQuery;

        [ObservableProperty]
        private User? user;

        [ObservableProperty]
        private ObservableCollection<Performance> performances = new();

        [ObservableProperty]
        private string customerName = string.Empty;

        [ObservableProperty]
        private string customerEmail = string.Empty;

        [ObservableProperty]
        private Performance? selectedPerformance;

        public BookingPageViewModel(IUserService userService, IPerformanceService performanceService, IBookingService bookingService)
        {
            _userService = userService;
            _performanceService = performanceService;
            _bookingService = bookingService;
        }

        public async Task InitializeAsync()
        {
            await LoadUserDataAsync();
            await LoadPerformancesAsync();
        }

        [RelayCommand]
        private async Task LoadUserDataAsync()
        {
            if (!string.IsNullOrEmpty(UserIdQuery))
            {
                try
                {
                    User = await _userService.GetUserByIdAsync(UserIdQuery);
                    if (User != null)
                    {
                        CustomerName = User.Name;
                        CustomerEmail = User.Email;
                    }
                }
                catch (HttpRequestException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"HTTP ERROR: {ex.Message}");
                }
            }
        }

        [RelayCommand]
        private async Task LoadPerformancesAsync()
        {
            if (!string.IsNullOrEmpty(PerformanceIdsQuery))
            {
                try
                {
                    var performanceIds = PerformanceIdsQuery.Split(',');
                    Performances.Clear();
                    foreach (var id in performanceIds)
                    {
                        var performance = await _performanceService.GetPerformanceByIdAsync(id);
                        if (performance != null)
                        {
                            Performances.Add(performance);
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"HTTP ERROR: {ex.Message}");
                }
            }
        }

        [RelayCommand]
        public async Task ConfirmBookingAsync()
        {
            if (SelectedPerformance == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select a performance!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(CustomerEmail))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please provide customer name and email!", "OK");
                return;
            }

            if (await _bookingService.BookPerformanceAsync(SelectedPerformance.Id, CustomerName, CustomerEmail))
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Booking confirmed!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Booking failed!", "OK");
            }
        }

        [RelayCommand]
        public async Task CancelBookingAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}