using FastFlat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FastFlat.ViewModels
{
    public class ProfileViewModel
    {
        public IEnumerable<ListningModel> Rentals;
        public IEnumerable<BookingModel> Bookings;
        public ApplicationUser User;

        public string? CurrentViewName;

        public ProfileViewModel(IEnumerable<ListningModel> rentals, IEnumerable<BookingModel> bookings, ApplicationUser user, string? currentViewName)
        {
            Rentals = rentals;
            Bookings = bookings;
            User = user;
            CurrentViewName = currentViewName;

        }
    }
}