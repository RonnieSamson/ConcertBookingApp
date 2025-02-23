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

        private ObservableCollection<Concert.MAUI.Models.Concert> _concerts = new();

        public ObservableCollection<Concert.MAUI.Models.Concert> Concerts
        {
            get => _concerts;
            set => SetProperty(ref _concerts, value);
        }

        private ObservableCollection<Performance> _selectedPerformances = new();

        public ObservableCollection<Performance> SelectedPerformances
        {
            get => _selectedPerformances;
            set => SetProperty(ref _selectedPerformances, value);
        }

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
                //Concerts = new ObservableCollection<Concert.MAUI.Models.Concert>(Concerts);
            }
        }
        [RelayCommand]
        private async Task LoadPerformances(string concertId)
        {
            Console.WriteLine($"🔍 LoadPerformancesCommand körs för ConcertId: {concertId}");

            var performancesList = await _performanceService.GetPerformancesByConcertIdAsync(concertId);
            if (performancesList == null || !performancesList.Any())
            {
                Console.WriteLine($"❌ Inga performances hittades för ConcertId: {concertId}");
                return;
            }

            // 🧹 Rensa tidigare performances innan vi lägger till nya
            SelectedPerformances.Clear();
            foreach (var performance in performancesList)
            {
                SelectedPerformances.Add(performance);
            }

            Console.WriteLine($"✅ {SelectedPerformances.Count} performances inlagda i listan");

            // 🔄 Uppdatera UI
            OnPropertyChanged(nameof(SelectedPerformances));
        }



        //[RelayCommand]
        private async Task BookPerformance(string concertId, string userId)
        {
            bool sucess = await _bookingService.BookPerformanceAsync(userId, concertId);
            if (sucess)
            {
                Console.WriteLine($"✅ Bokning lyckades för konsert {concertId} av användare {userId}");
            }
        }
    }
}
