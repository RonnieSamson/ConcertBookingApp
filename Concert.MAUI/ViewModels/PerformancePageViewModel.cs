using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Concert.MAUI.Services;
using Concert.MAUI.Models;
using Concert.MAUI.Views;

namespace Concert.MAUI.ViewModels
{
    [QueryProperty(nameof(ConcertId), nameof(ConcertId))]
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public partial class PerformancePageViewModel : ObservableObject
    {
        private readonly IPerformanceService _performanceService;

        [ObservableProperty]
        public partial ObservableCollection<Performance> Performances { get; set; }

        [ObservableProperty]
        public partial ObservableCollection<object> SelectedPerformances { get; set; }

        [ObservableProperty]
        public partial string ConcertId { get; set; }

        [ObservableProperty]
        public partial string UserId { get; set; }

        public PerformancePageViewModel(IPerformanceService performanceService)
        {
            _performanceService = performanceService;
            Performances = new ObservableCollection<Performance>();
            SelectedPerformances = new ObservableCollection<object>();
            ConcertId = string.Empty;
            UserId = string.Empty;
        }

        partial void OnConcertIdChanged(string value)
        {
            System.Diagnostics.Debug.WriteLine($"🎬 PerformancePageViewModel.OnConcertIdChanged: '{value}'");
            _ = LoadPerformancesAsync();
        }


       

        public async Task LoadPerformancesAsync()
        {
            System.Diagnostics.Debug.WriteLine($"🎬 PerformancePageViewModel.LoadPerformancesAsync starting with ConcertId: '{ConcertId}'");
            
            var performanceList = await _performanceService.GetPerformancesByConcertIdAsync(ConcertId);
            
            System.Diagnostics.Debug.WriteLine($"🎬 PerformancePageViewModel.LoadPerformancesAsync got {performanceList?.Count() ?? 0} performances");
            
            if (performanceList != null)
            {
                Performances.Clear();
                foreach (var perf in performanceList)
                {
                    Performances.Add(perf);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"🎬 PerformancePageViewModel.LoadPerformancesAsync: No performances found for concertId '{ConcertId}'");
            }
        }

        [RelayCommand]
        public async Task BookSelectedPerformancesAsync()
        {
            System.Diagnostics.Debug.WriteLine("=== BookSelectedPerformancesAsync called ===");
            System.Diagnostics.Debug.WriteLine($"SelectedPerformances count: {SelectedPerformances?.Count ?? 0}");
            
            // Kontrollera om några performances är valda
            if (SelectedPerformances == null || !SelectedPerformances.Any())
            {
                System.Diagnostics.Debug.WriteLine("No performances selected!");
                await Shell.Current.DisplayAlert("Error", "Please select at least one performance", "OK");
                return;
            }

            // Konvertera objekt till Performance och hämta deras Id
            var selectedPerformanceIds = string.Join(",",
                SelectedPerformances.Cast<Performance>().Select(p => p.Id));
                
            System.Diagnostics.Debug.WriteLine($"Selected performance IDs: {selectedPerformanceIds}");
            System.Diagnostics.Debug.WriteLine($"UserId: {UserId}");
            System.Diagnostics.Debug.WriteLine($"ConcertId: {ConcertId}");

            // Bygg en dictionary med dina parametrar
            var routeParameters = new Dictionary<string, object>
            {
                { "UserIdQuery", UserId },
                { "ConcertIdQuery", ConcertId },
                { "PerformanceIdsQuery", selectedPerformanceIds }
            };

            System.Diagnostics.Debug.WriteLine("Navigating to BookingPage...");
            // Navigera till BookingPage
            await Shell.Current.GoToAsync(nameof(BookingPage), routeParameters);
        }




    }
}