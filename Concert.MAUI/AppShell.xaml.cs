using Concert.MAUI.Views;

namespace Concert.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PerformanceDetailsPage), typeof(PerformanceDetailsPage));
            Routing.RegisterRoute(nameof(BookingPage), typeof(BookingPage));

        }
    }
}
