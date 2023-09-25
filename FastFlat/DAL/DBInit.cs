using FastFlat.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.DAL
{
    public class DBInit
    { 
        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            RentalDbContext context = serviceScope.ServiceProvider.GetRequiredService<RentalDbContext>();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //City
            if(!context.Cities.Any())
            {
                var cities = new List<CityModel>
                {
                    new CityModel
                    {
                        CityName="Oslo",
                        Country="Norway"
                    },

                    new CityModel
                    {
                        CityName="Bergen",
                        Country="Norway"
                    },

                    new CityModel
                    {
                        CityName="Trondheim",
                        Country="Norway"
                    },

                    new CityModel
                    {
                        CityName="Stockholm",
                        Country="Sweden"
                    },

                    new CityModel
                    {
                        CityName="Stockholm",
                        Country="Gothenburg"
                    },
                };
                context.AddRange(cities);
                context.SaveChanges();
            }

            //Country
            if (!context.Countries.Any())
            {
                var country = new List<ContryModel>
                {
                    new ContryModel
                    {
                        Contryname="Norway"
                    },

                    new ContryModel
                    {
                        Contryname="Sweden"
                    },

                    new ContryModel
                    {
                        Contryname="Denmark"
                    },

                };
                context.AddRange(country);
                context.SaveChanges();
            }



            if (!context.Users.Any())
            {
                var users = new List<UserModel>
                {
                    new UserModel
                    {   
                        Username = "Olidrav",
                        FirstName="Oliver",
                        LastName="Dragland",
                        Email="oliver@gmail.com",
                        Password="1234",
                        Phone=99999999,
                        ProfilePicture = "/images/profilepicture/oliver.jpg",
                        Rentals=new List<ListningModel>{},
                        Bookings=new List<BookingModel>{},
                    },

                    new UserModel
                    {
                        Username = "JP",
                        FirstName="Jon",
                        LastName="Petter",
                        Email="jp@gmail.com",
                        Password="1234",
                        Phone=9988888,
                        ProfilePicture = "/images/profilepicture/jp.jpg",
                        Rentals=new List<ListningModel>{},
                        Bookings=new List<BookingModel>{},
                    },

                    new UserModel
                    {
                        Username = "Gistrong",
                        FirstName="Gisle",
                        LastName="Na",
                        Email="Gisle@gmail.com",
                        Password="1234",
                        Phone=99777777,
                        ProfilePicture = "/images/profilepicture/gisle.jpg",
                        Rentals=new List<ListningModel>{},
                        Bookings=new List<BookingModel>{},
                    },

                    new UserModel
                    {
                        Username = "Alinam",
                        FirstName="Ali",
                        LastName="Anjum",
                        Email="Ali@gmail.com",
                        Password="1234",
                        Phone=99666666,
                        ProfilePicture = "/images/profilepicture/ali.jpg",
                        Rentals=new List<ListningModel>{},
                        Bookings=new List<BookingModel>{},
                    }
                };
                context.AddRange(users);
                context.SaveChanges();
            }

            if (!context.Rentals.Any())
            {
                var listnings = new List<ListningModel>
    {
        new ListningModel
        {
            user = context.Users.FirstOrDefault(u => u.Username == "Alinam"), // Linker denne eiendommen til brukeren 'Alinam'
            ListningName = "Sentrum Leilighet",
            ListningDescription = "Moderne leilighet i Oslo sentrum med flott utsikt over byen.",
            NoOfBeds = 2,
            SquareMeter = 75,
            Rating = 4.5f,
            ListningAddress = "Osloveien 123, 0456 Oslo",
            ListningPrice = 2000,
            fromDate = DateOnly.FromDateTime(DateTime.Today),
            toDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
            ListningImageURL = "/images/rental2.png"
        },
        new ListningModel
        {
            user = context.Users.FirstOrDefault(u => u.Username == "Alinam"), // Linker denne eiendommen til brukeren 'Alinam'
            ListningName = "Fjellhytte",
            ListningDescription = "Koselig hytte i fjellet, perfekt for vinterferier.",
            NoOfBeds = 5,
            SquareMeter = 100,
            Rating = 4.8f,
            ListningAddress = "Fjellveien 567, 1234 Fjellby",
            ListningPrice = 3000,
            fromDate = DateOnly.FromDateTime(DateTime.Today),
            toDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
            ListningImageURL = "/images/rental1.png"
        }
    };

                context.AddRange(listnings);
                context.SaveChanges();
            }
        }
        
    }
}
