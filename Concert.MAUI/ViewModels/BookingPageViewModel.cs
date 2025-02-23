using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Concert.MAUI.Models;
using Concert.MAUI.Services;

namespace Concert.MAUI.ViewModels
{
    [QueryProperty(nameof(UserIdQuery), nameof(UserIdQuery))]
    [QueryProperty(nameof(ConcertIdQuery), nameof(ConcertIdQuery))]
    [QueryProperty(nameof(PerformanceIdsQuery), nameof(PerformanceIdsQuery))]
    public partial class BookingPageViewModel : ObservableObject
    {
        private readonly IUserService _userService;
        private readonly IPerformanceService _performanceService;
        private readonly IBookingService _bookingService;

        [ObservableProperty]
        private string userIdQuery;

        [ObservableProperty]
        private string concertIdQuery;

        [ObservableProperty]
        private string performanceIdsQuery;

        [ObservableProperty]
        private User user;

        [ObservableProperty]
        private ObservableCollection<Performance> performances = new();

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
            if (await _bookingService.BookPerformanceAsync(User.Id, ConcertIdQuery))
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Booking confirmed!", "OK");
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