using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Concert.MAUI.Services;
using Concert.MAUI.Models;

namespace Concert.MAUI.ViewModels
{
    [QueryProperty(nameof(ConcertId), nameof(ConcertId))]
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public partial class PerformancePageViewModel : ObservableObject
    {
        private readonly IPerformanceService _performanceService;

        [ObservableProperty]
        private ObservableCollection<Performance> performances = new();

        [ObservableProperty]
        private ObservableCollection<Performance> selectedPerformances = new();

        [ObservableProperty]
        private string concertId;

        [ObservableProperty]
        private string userId;

        public PerformancePageViewModel(IPerformanceService performanceService)
        {
            _performanceService = performanceService;
        }

        partial void OnConcertIdChanged(string value)
        {
            _ = LoadPerformancesAsync();
        }

        public async Task LoadPerformancesAsync()
        {
            var performanceList = await _performanceService.GetPerformancesByConcertIdAsync(ConcertId);
            if (performanceList != null)
            {
                Performances.Clear();
                foreach (var perf in performanceList)
                {
                    Performances.Add(perf);
                }
            }
        }

        // Använd [RelayCommand] för att automatiskt generera kommandot med namnet BookSelectedPerformancesCommand.
        [RelayCommand]
        public async Task BookSelectedPerformancesAsync()
        {
            // Kontrollera om några performances är valda
            if (SelectedPerformances == null || !SelectedPerformances.Any())
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please select at least one performance", "OK");
                return;
            }

            // Skapa en kommaseparerad sträng av valda performance IDs
            var selectedPerformanceIds = string.Join(",", SelectedPerformances.Select(p => p.Id));

            // Bygg en dictionary med dina parametrar
            var routeParameters = new Dictionary<string, object>
            {
                { "UserIdQuery", UserId },
                { "ConcertIdQuery", ConcertId },
                { "PerformanceIdsQuery", selectedPerformanceIds }
            };

            await Shell.Current.GoToAsync($"///BookingPage", routeParameters);
        }
    }
}