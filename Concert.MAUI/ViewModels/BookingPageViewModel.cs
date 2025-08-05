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
        private readonly IAuthenticationService _authService;

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

        public BookingPageViewModel(IUserService userService, IPerformanceService performanceService, IBookingService bookingService, IAuthenticationService authService)
        {
            _userService = userService;
            _performanceService = performanceService;
            _bookingService = bookingService;
            _authService = authService;
            
            // Initialize properties with default values
            Performances = new ObservableCollection<Performance>();
            CustomerName = string.Empty;
            CustomerEmail = string.Empty;
            
            // Auto-fill with logged-in user information
            if (_authService.IsAuthenticated)
            {
                CustomerName = _authService.CurrentUserName ?? string.Empty;
                CustomerEmail = _authService.CurrentUserEmail ?? string.Empty;
            }
        }

        public async Task InitializeAsync()
        {
            System.Diagnostics.Debug.WriteLine("=== BookingPageViewModel InitializeAsync called ===");
            System.Diagnostics.Debug.WriteLine($"UserIdQuery: {UserIdQuery}");
            System.Diagnostics.Debug.WriteLine($"PerformanceIdsQuery: {PerformanceIdsQuery}");
            
            await LoadUserDataAsync();
            await LoadPerformancesAsync();
            
            System.Diagnostics.Debug.WriteLine($"Final Performances count: {Performances.Count}");
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
            System.Diagnostics.Debug.WriteLine("=== LoadPerformancesAsync called ===");
            System.Diagnostics.Debug.WriteLine($"PerformanceIdsQuery: '{PerformanceIdsQuery}'");
            
            if (!string.IsNullOrEmpty(PerformanceIdsQuery))
            {
                try
                {
                    var performanceIds = PerformanceIdsQuery.Split(',');
                    System.Diagnostics.Debug.WriteLine($"Performance IDs to load: {string.Join(", ", performanceIds)}");
                    
                    Performances.Clear();
                    foreach (var id in performanceIds)
                    {
                        System.Diagnostics.Debug.WriteLine($"Loading performance with ID: '{id.Trim()}'");
                        var performance = await _performanceService.GetPerformanceByIdAsync(id.Trim());
                        if (performance != null)
                        {
                            System.Diagnostics.Debug.WriteLine($"Loaded performance: {performance.Location} at {performance.StartTime}");
                            Performances.Add(performance);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Performance with ID '{id.Trim()}' was null!");
                        }
                    }
                    System.Diagnostics.Debug.WriteLine($"Total performances loaded: {Performances.Count}");
                    
                    System.Diagnostics.Debug.WriteLine($"Performances ready for booking confirmation: {Performances.Count}");
                }
                catch (HttpRequestException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"HTTP ERROR: {ex.Message}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("PerformanceIdsQuery is null or empty!");
            }
        }

        [RelayCommand]
        public async Task ConfirmBookingAsync()
        {
            if (!Performances.Any())
            {
                await Shell.Current.DisplayAlert("Error", "No performances available to book!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(CustomerEmail))
            {
                await Shell.Current.DisplayAlert("Error", "Please provide customer name and email!", "OK");
                return;
            }

            // Check if user has already booked any of these performances
            var existingBookings = await _bookingService.GetBookingsByEmailAsync(CustomerEmail);
            var alreadyBookedPerformances = new List<Performance>();
            
            if (existingBookings != null)
            {
                foreach (var performance in Performances)
                {
                    if (existingBookings.Any(b => b.PerformanceId == performance.Id))
                    {
                        alreadyBookedPerformances.Add(performance);
                    }
                }
            }

            // If some performances are already booked, ask user what to do
            if (alreadyBookedPerformances.Any())
            {
                var alreadyBookedNames = string.Join(", ", alreadyBookedPerformances.Select(p => p.Location));
                bool continueWithRest = await Shell.Current.DisplayAlert("Some Already Booked", 
                    $"You have already booked: {alreadyBookedNames}\n\n" +
                    "Do you want to book the remaining performances?", 
                    "Yes", "Cancel");
                
                if (!continueWithRest)
                {
                    return;
                }
            }

            // Get performances that are not already booked
            var performancesToBook = Performances.Where(p => 
                existingBookings == null || !existingBookings.Any(b => b.PerformanceId == p.Id)).ToList();

            if (!performancesToBook.Any())
            {
                await Shell.Current.DisplayAlert("Already Booked", 
                    "You have already booked all selected performances.", "OK");
                return;
            }

            // Book all unbooked performances
            var successfulBookings = new List<string>();
            var failedBookings = new List<string>();

            foreach (var performance in performancesToBook)
            {
                System.Diagnostics.Debug.WriteLine($"Booking performance: {performance.Location} for {CustomerName} ({CustomerEmail})");
                
                if (await _bookingService.BookPerformanceAsync(performance.Id, CustomerName, CustomerEmail))
                {
                    successfulBookings.Add(performance.Location);
                }
                else
                {
                    failedBookings.Add(performance.Location);
                }
            }

            // Show results
            var message = "";
            if (successfulBookings.Any())
            {
                message += $"Successfully booked: {string.Join(", ", successfulBookings)}";
            }
            if (failedBookings.Any())
            {
                if (!string.IsNullOrEmpty(message)) message += "\n\n";
                message += $"Failed to book: {string.Join(", ", failedBookings)}";
            }

            await Shell.Current.DisplayAlert("Booking Results", message, "OK");
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task CancelBookingAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}