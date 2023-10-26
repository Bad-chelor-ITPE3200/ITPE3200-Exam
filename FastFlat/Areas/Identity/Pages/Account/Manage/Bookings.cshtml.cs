
using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastFlat.Areas.Identity.Pages.Account.Manage
{
    public class BookingsModel : PageModel
    {
        private readonly IRentalRepository<BookingModel> _bookingRepository;
        private readonly IRentalRepository<ListningModel> _rentalRepository;
        public IEnumerable<BookingViewModel> Bookings { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;

        public BookingsModel(IRentalRepository<BookingModel> bookingRepository, UserManager<ApplicationUser> userManager, IRentalRepository<ListningModel> rentalRepository)
        {
            _bookingRepository = bookingRepository;
            _userManager = userManager;
            _rentalRepository = rentalRepository;
        }
        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var userBookings = (_bookingRepository.GetAll()).Where(b => b.UserId == user.Id).ToList();
            var bookingsExtended = new List<BookingViewModel>();
            foreach (var booking in userBookings)
            {
                var listing = await _rentalRepository.GetById(booking.ListningId);
                var model = new BookingViewModel(listing, user, booking, "Bookings");
                bookingsExtended.Add(model);
            }

            Bookings = bookingsExtended;
        }
    }
}
