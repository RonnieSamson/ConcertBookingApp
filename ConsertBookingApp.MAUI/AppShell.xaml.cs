using ConcertBookingApp.MAUI.View;

namespace ConcertBookingApp.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("concerts", typeof(ConcertPage));
        }
    }
}
