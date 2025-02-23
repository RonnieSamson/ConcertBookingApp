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

        private ObservableCollection<Concert.MAUI.Models.Concert> _concerts = new();

        public ObservableCollection<Concert.MAUI.Models.Concert> Concerts
        {
            get => _concerts;
            set => SetProperty(ref _concerts, value);
        }

        [ObservableProperty]
        private string userId;

        public HomepageViewModel(IConcertService concertService, IPerformanceService performanceService, IBookingService bookingService)
        {
            _concertService = concertService;
            _performanceService = performanceService;
            _bookingService = bookingService;

            Task.Run(async () => await LoadConcerts());
        }

        [RelayCommand]
        private async Task LoadConcerts()
        {
            var concertsList = await _concertService.GetAllConcertsAsync();
            if (concertsList != null)
            {
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
            await Shell.Current.GoToAsync($"///PerformanceDetailsPage?ConcertId={concertId}&UserId={UserId}");
        }
    }
}