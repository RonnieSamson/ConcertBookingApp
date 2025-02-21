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

        [ObservableProperty]
        private ObservableCollection<Concert.MAUI.Models.Concert> concerts = new();

        public HomepageViewModel(IConcertService concertService)
        {
            _concertService = concertService;
            
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
    }
}
