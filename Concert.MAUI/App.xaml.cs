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
            return new Window(new AppShell());
        }
    }
}