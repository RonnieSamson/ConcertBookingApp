using ConcertBookingApp.MAUI.Models;
using ConcertBookingApp.MAUI.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ConcertBookingApp.MAUI.ViewModel
{
    public partial class ConcertViewModel : ObservableObject
    {
        [ObservableProperty]
        public string message = "Hello World";
      
    }
}
