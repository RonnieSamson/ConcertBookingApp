using ConcertBookingApp.MAUI.ViewModel;

namespace ConcertBookingApp.MAUI.View;

public partial class ConcertPage : ContentPage
{
	public ConcertPage(ConcertViewModel vm)
	{
        try
        {
            InitializeComponent();
            BindingContext = vm;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid instansiering av ConsertViewModel: {ex.Message}");
            throw;
        }
    }
}