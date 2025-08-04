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
        public partial string? UserIdQuery { get; set; }

        [ObservableProperty]
        public partial string? PerformanceIdsQuery { get; set; }

        [ObservableProperty]
        public partial User? User { get; set; }

        [ObservableProperty]
        public partial ObservableCollection<Performance> Performances { get; set; }

        [ObservableProperty]
        public partial string CustomerName { get; set; }

        [ObservableProperty]
        public partial string CustomerEmail { get; set; }

        [ObservableProperty]
        public partial Performance? SelectedPerformance { get; set; }

        public BookingPageViewModel(IUserService userService, IPerformanceService performanceService, IBookingService bookingService)
        {
            _userService = userService;
            _performanceService = performanceService;
            _bookingService = bookingService;
            
            // Initialize properties with default values
            Performances = new ObservableCollection<Performance>();
            CustomerName = string.Empty;
            CustomerEmail = string.Empty;
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
                await Shell.Current.DisplayAlert("Error", "Please select a performance!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(CustomerEmail))
            {
                await Shell.Current.DisplayAlert("Error", "Please provide customer name and email!", "OK");
                return;
            }

            if (await _bookingService.BookPerformanceAsync(SelectedPerformance.Id, CustomerName, CustomerEmail))
            {
                await Shell.Current.DisplayAlert("Success", "Booking confirmed!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Booking failed!", "OK");
            }
        }

        [RelayCommand]
        public async Task CancelBookingAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}