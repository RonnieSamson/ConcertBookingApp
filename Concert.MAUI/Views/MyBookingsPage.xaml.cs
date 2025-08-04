using Concert.MAUI.ViewModels;

namespace Concert.MAUI.Views;

public partial class MyBookingsPage : ContentPage
{
    private readonly MyBookingsViewModel _viewModel;

    public MyBookingsPage(MyBookingsViewModel viewModel)
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
