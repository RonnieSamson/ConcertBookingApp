using Concert.MAUI.Services;
using Concert.MAUI.Views;

namespace Concert.MAUI
{
    public partial class AppShell : Shell
    {
        private readonly IAuthenticationService _authService;

        public AppShell(IAuthenticationService authService)
        {
            InitializeComponent();
            
            _authService = authService;
            _authService.AuthenticationChanged += OnAuthenticationChanged;
            
            // Register routes
            Routing.RegisterRoute(nameof(PerformanceDetailsPage), typeof(PerformanceDetailsPage));
            Routing.RegisterRoute(nameof(BookingPage), typeof(BookingPage));
            Routing.RegisterRoute(nameof(MyBookingsPage), typeof(MyBookingsPage));
            
            // Set initial navigation based on authentication status
            UpdateNavigationVisibility();
        }

        private void OnAuthenticationChanged(object? sender, AuthenticationChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UpdateNavigationVisibility();
                
                if (e.IsAuthenticated)
                {
                    // Navigate to home page when authenticated
                    Shell.Current.GoToAsync("//home");
                }
                else
                {
                    // Navigate to login when not authenticated
                    Shell.Current.GoToAsync("//login");
                }
            });
        }

        private void UpdateNavigationVisibility()
        {
            // Show/hide main tab bar based on authentication
            MainTabBar.IsVisible = _authService.IsAuthenticated;
        }

        protected override void OnDisappearing()
        {
            if (_authService != null)
                _authService.AuthenticationChanged -= OnAuthenticationChanged;
            base.OnDisappearing();
        }
    }
}
