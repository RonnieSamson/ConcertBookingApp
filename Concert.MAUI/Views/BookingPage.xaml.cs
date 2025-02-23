using Concert.MAUI.ViewModels;

namespace Concert.MAUI.Views;

public partial class BookingPage : ContentPage
{
    

    public BookingPage(BookingPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
    }
}