using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Concert.MAUI.Models;
using Concert.MAUI.Services;
using Concert.MAUI.Views;
using Microsoft.Maui.Controls;

namespace Concert.MAUI.ViewModels
{
    public partial class HomepageViewModel : ObservableObject
    {
        private readonly IConcertService _concertService;
        private readonly IPerformanceService _performanceService;
        private readonly IBookingService _bookingService;
        private readonly IAuthenticationService _authService;

        private ObservableCollection<Concert.MAUI.Models.Concert> _concerts = new();

        public ObservableCollection<Concert.MAUI.Models.Concert> Concerts
        {
            get => _concerts;
            set => SetProperty(ref _concerts, value);
        }

        [ObservableProperty]
        public partial string WelcomeMessage { get; set; }

        public HomepageViewModel(IConcertService concertService, IPerformanceService performanceService, 
                               IBookingService bookingService, IAuthenticationService authService)
        {
            _concertService = concertService;
            _performanceService = performanceService;
            _bookingService = bookingService;
            _authService = authService;
            
            WelcomeMessage = "Welcome to Concert Booking App";
            UpdateWelcomeMessage();
            _ = LoadConcerts(); // Call LoadConcerts on the UI thread
        }

        private void UpdateWelcomeMessage()
        {
            var userName = _authService.CurrentUserName ?? "User";
            WelcomeMessage = $"Welcome, {userName}!";
        }

        [RelayCommand]
        private async Task LoadConcerts()
        {
            System.Diagnostics.Debug.WriteLine("🔄 LoadConcerts() started");
            
            try
            {
                var concertsList = await _concertService.GetAllConcertsAsync();
                System.Diagnostics.Debug.WriteLine($"📊 API returned {concertsList?.Count ?? 0} concerts");
                
                if (concertsList != null)
                {
                    // Update UI on main thread
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        // Clear existing concerts to prevent duplicates
                        Concerts.Clear();
                        foreach (var concert in concertsList)
                        {
                            System.Diagnostics.Debug.WriteLine($"➕ Adding concert: {concert.Title}");
                            Concerts.Add(concert);
                        }
                        System.Diagnostics.Debug.WriteLine($"✅ Total concerts in UI: {Concerts.Count}");
                    });
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("❌ No concerts returned from API");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"💥 Error loading concerts: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task ShowPerformances(string concertId)
        {
            System.Diagnostics.Debug.WriteLine($"ShowPerformances called with concertId: {concertId}");
            
            var userId = _authService.CurrentUserId;
            System.Diagnostics.Debug.WriteLine($"Current userId: {userId}");
            
            var navigationUrl = $"{nameof(PerformanceDetailsPage)}?ConcertId={concertId}&UserId={userId}";
            System.Diagnostics.Debug.WriteLine($"Navigating to: {navigationUrl}");
            
            try
            {
                await Shell.Current.GoToAsync(navigationUrl);
                System.Diagnostics.Debug.WriteLine("Navigation completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation failed: {ex.Message}");
                await Shell.Current.DisplayAlert("Navigation Error", $"Failed to navigate: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task ViewMyBookings()
        {
            // Use tab navigation instead of page navigation
            await Shell.Current.GoToAsync("//bookings");
        }

        [RelayCommand]
        private async Task Logout()
        {
            await _authService.LogoutAsync();
        }
    }
}