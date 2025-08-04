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
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public partial class HomepageViewModel : ObservableObject
    {
        private readonly IConcertService _concertService;
        private readonly IPerformanceService _performanceService;
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        private ObservableCollection<Concert.MAUI.Models.Concert> _concerts = new();

        public ObservableCollection<Concert.MAUI.Models.Concert> Concerts
        {
            get => _concerts;
            set => SetProperty(ref _concerts, value);
        }

        [ObservableProperty]
        public partial string? UserId { get; set; }

        [ObservableProperty]
        public partial string WelcomeMessage { get; set; }

        public HomepageViewModel(IConcertService concertService, IPerformanceService performanceService, IBookingService bookingService, IUserService userService)
        {
            _concertService = concertService;
            _performanceService = performanceService;
            _bookingService = bookingService;
            _userService = userService;
            WelcomeMessage = "Welcome to Concert Booking App";

            Task.Run(async () => await LoadConcerts());
        }

        partial void OnUserIdChanged(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Task.Run(async () => await LoadUserInfo());
            }
        }

        private async Task LoadUserInfo()
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                var user = await _userService.GetUserByIdAsync(UserId);
                if (user != null)
                {
                    WelcomeMessage = $"Welcome, {user.Name}!";
                }
            }
        }

        [RelayCommand]
        private async Task LoadConcerts()
        {
            var concertsList = await _concertService.GetAllConcertsAsync();
            if (concertsList != null)
            {
                // Clear existing concerts to prevent duplicates
                Concerts.Clear();
                foreach (var concert in concertsList)
                {
                    Concerts.Add(concert);
                }
            }
        }

        [RelayCommand]
        private async Task ShowPerformances(string concertId)
        {
            await Shell.Current.GoToAsync($"{nameof(PerformanceDetailsPage)}?ConcertId={concertId}&UserId={UserId}");
        }

        [RelayCommand]
        private async Task ViewMyBookings()
        {
            await Shell.Current.GoToAsync($"{nameof(MyBookingsPage)}");
        }

        [RelayCommand]
        private async Task Logout()
        {
            await Shell.Current.GoToAsync("//login");
        }
    }
}