using Concert.DTO;
using Concert.MAUI.Services;
using System.Collections.ObjectModel;

namespace Concert.MAUI.Views;

public partial class ConcertsPage : ContentPage
{
    private readonly IConcertService _concertService;

    public ObservableCollection<ConcertDto> Concerts { get; set; } = new();

    public ConcertsPage(IConcertService concertService)
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
            // Hämta konserter direkt som DTO
            var concerts = await _concertService.GetConcertsAsync();

            // Rensa och fyll på ObservableCollection
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