using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Concert.MAUI.Models;
using Concert.MAUI.Services;

namespace Concert.MAUI.ViewModels
{
    public partial class MyBookingsViewModel : ObservableObject
    {
        private readonly IBookingService _bookingService;

        [ObservableProperty]
        public partial string CustomerEmail { get; set; }

        [ObservableProperty]
        public partial ObservableCollection<Booking> Bookings { get; set; }

        public MyBookingsViewModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
            CustomerEmail = string.Empty;
            Bookings = new ObservableCollection<Booking>();
        }

        public Task InitializeAsync()
        {
            // Return completed task since no async work is done
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task LoadBookings()
        {
            if (string.IsNullOrEmpty(CustomerEmail))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter your email address!", "OK");
                return;
            }

            try
            {
                var userBookings = await _bookingService.GetBookingsByEmailAsync(CustomerEmail);
                Bookings.Clear();
                if (userBookings != null)
                {
                    foreach (var booking in userBookings)
                    {
                        Bookings.Add(booking);
                    }
                }

                if (!Bookings.Any())
                {
                    await Shell.Current.DisplayAlert("Info", "No bookings found for this email.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load bookings: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task CancelBooking(string bookingId)
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Confirm Cancellation",
                "Are you sure you want to cancel this booking?",
                "Yes", "No");

            if (confirm)
            {
                if (await _bookingService.CancelBookingAsync(bookingId))
                {
                    await Shell.Current.DisplayAlert("Success", "Booking cancelled successfully!", "OK");
                    // Reload bookings to reflect the change
                    await LoadBookings();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Failed to cancel booking!", "OK");
                }
            }
        }
    }
}
