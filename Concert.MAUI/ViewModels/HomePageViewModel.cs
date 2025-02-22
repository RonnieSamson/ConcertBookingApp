using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Concert.MAUI.Models;
using Concert.MAUI.Services;

namespace Concert.MAUI.ViewModels
{
    public partial class HomepageViewModel : ObservableObject
    {
        private readonly IConcertService _concertService;
        private readonly IPerformanceService _performanceService;
        private readonly IBookingService _bookingService;

        [ObservableProperty]
        private ObservableCollection<Concert.MAUI.Models.Concert> concerts = new();

        [ObservableProperty]
        private Dictionary<string, ObservableCollection<Performance>> concertPerformances = new();

        public HomepageViewModel(IConcertService concertService, IPerformanceService performanceService, IBookingService bookingService)
        {
            _concertService = concertService;
            _performanceService = performanceService;
            _bookingService = bookingService;

        }

        [RelayCommand]
        private async Task LoadConcerts()
        {
            var concertsList = await _concertService.GetAllConcertsAsync();
            if (concertsList != null)
            {
                Concerts.Clear();
                concertPerformances.Clear();
                foreach (var concert in concertsList)
                {
                    Concerts.Add(concert);
                    concertPerformances[concert.ConcertId] = new ObservableCollection<Performance>();
                }
            }
        }
        [RelayCommand]
        private async Task LoadPerformances(string concertId)
        {
            if (concertPerformances.ContainsKey(concertId) && concertPerformances[concertId].Count == 0)
            {
                var performances = await _performanceService.GetPerformancesByConcertIdAsync(concertId);
                if (performances != null)
                {
                    foreach (var performance in performances)
                    {
                        concertPerformances[concertId].Add(performance);
                    }
                }
            }
        }

        //[RelayCommand]
        private async Task BookPerformance(string concertId, string userId)
        {
            bool sucess = await _bookingService.BookPerformanceAsync(userId, concertId);
            if (sucess)
            {
                
            }
        }
    }
}
