using FastFlat.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.DAL
{
    public class DBInit
    { /*
        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            RentalDbContext context = serviceScope.ServiceProvider.GetRequiredService<RentalDbContext>();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();


            if (!context.Users.Any())
            {
                var users = new List<UserModel>
                {
                    new UserModel
                    {   
                        Username = "OliverDragland",
                        FirstName="Oliver",
                        LastName="Dragland",
                        Email="oliver@gmail.com",
                        Password="1234",
                        Rentals=new List<ListningModel>{},
                        Bookings=new List<BookingModel>{},
                    }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
        */
    }
}
