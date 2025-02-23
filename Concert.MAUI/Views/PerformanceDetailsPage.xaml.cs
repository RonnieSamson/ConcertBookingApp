using Concert.MAUI.ViewModels;

namespace Concert.MAUI.Views;

public partial class PerformanceDetailsPage : ContentPage
{
    public PerformanceDetailsPage(PerformancePageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}