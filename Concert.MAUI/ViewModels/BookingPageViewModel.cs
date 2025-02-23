using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Concert.MAUI.Services;

namespace Concert.MAUI.ViewModels
{
    
    [QueryProperty(nameof(UserIdQuery), nameof(UserIdQuery))]
    [QueryProperty(nameof(ConcertIdQuery), nameof(ConcertIdQuery))]
    [QueryProperty(nameof(PerformanceIdsQuery), nameof(PerformanceIdsQuery))]
    public partial class BookingPageViewModel : ObservableObject
    {
        private readonly IBookingService _bookingService;

        [ObservableProperty]
        private string userIdQuery;

        [ObservableProperty]
        private string concertIdQuery;

        [ObservableProperty]
        private string performanceIdsQuery;

        public BookingPageViewModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
    }
}