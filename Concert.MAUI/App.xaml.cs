using Concert.MAUI.Services;
using Concert.MAUI.Views;

namespace Concert.MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new LoginPage();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var appShell = Handler?.MauiContext?.Services?.GetService<AppShell>();
            return new Window(appShell ?? new AppShell(null!));
        }
    }
}