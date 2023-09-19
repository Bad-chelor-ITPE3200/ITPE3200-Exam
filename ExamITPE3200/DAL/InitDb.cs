using ExamITPE3200.Models;
namespace ExamITPE3200.DAL;

public class InitDb
{
    //here we can put filler data and load it to a database
    public static void seed(IApplicationBuilder app) //seeding 
    {
        using var scope = app.ApplicationServices.CreateScope();
        FastFlatDbContext context = scope.ServiceProvider.GetRequiredService<FastFlatDbContext>();
        if (!context.Bookings.Any())
        {
            var bookings = new List<BookingModel>();
            context.AddRange(bookings);
            context.SaveChanges();
        }

        if (!context.Cities.Any())
        {
            var Cites = new List<CityModel>
            {
                new CityModel
                {
                    cityName = "Oslo",
                    country = "Norway"
                },
                new CityModel
                {
                    cityName = "Trondheim",
                    country = "Norway"
                }, 
            };

            //  var cities = new List<CityModel>();
            context.AddRange(Cites);
            context.SaveChanges();
        }

        if (!context.Countries.Any())
        {
            var countries = new List<ContryModel>();
            context.AddRange(countries);
            context.SaveChanges();
        }

        if (!context.Landlord.Any())
        {
            var landord = new List<LandlordModel>();
            context.AddRange(landord);
            context.SaveChanges();
        }

        if (!context.Renters.Any())
        {
            var renters = new List<RenterModel>();
            context.AddRange(renters);
            context.SaveChanges();
        }

        if (!context.Listings.Any())
        {
            var listings = new List<ListingModel>();
            context.AddRange(listings);
            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            var users = new List<UserModel>(); // should prob not be in a pure list
            context.AddRange(users);
            context.SaveChanges();
        }
    }
}