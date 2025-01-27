using Concert.MAUI.Models;
using Concert.MAUI.Services;
using System.Collections.ObjectModel;

namespace Concert.MAUI.Views;

public partial class ConcertsPage : ContentPage
{
    private readonly ConcertService _concertService;

    public ObservableCollection<Concerts> Concerts { get; set; } = new();

    public ConcertsPage(ConcertService concertService)
    {
        InitializeComponent();
        _concertService = concertService;
        BindingContext = this;

        LoadConcerts();
    }

    private async void LoadConcerts()
    {
        try
        {
            var concerts = await _concertService.GetConcertsAsync();
            Concerts.Clear();
            foreach (var concert in concerts)
            {
                Concerts.Add(concert);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load concerts: {ex.Message}", "OK");
        }
    }
}