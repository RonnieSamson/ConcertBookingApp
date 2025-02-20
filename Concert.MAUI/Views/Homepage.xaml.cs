using Concert.MAUI.ViewModels;

namespace Concert.MAUI.Views;

public partial class Homepage : ContentPage
{
	public Homepage(HomepageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}