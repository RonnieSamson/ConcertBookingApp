using Concert.MAUI.ViewModels;

namespace Concert.MAUI.Views;

public partial class BookingPage : ContentPage
{
    private readonly BookingPageViewModel _viewModel;

    public BookingPage(BookingPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeAsync();
    }
}