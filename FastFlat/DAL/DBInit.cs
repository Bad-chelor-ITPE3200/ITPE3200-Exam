using FastFlat.Models;
using Microsoft.AspNetCore.Identity;
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

            //Amenity
            if (!context.Amenities.Any())
            {
                var amenities = new List<AmenityModel>
                {
                new AmenityModel
                    {
                        AmenityName="Bathtub",
                        AmenityDescription="A tub to relax in and take a bath.",
                        AmenityLogo="/images/amenity/Bathtub.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName="Beach",
                        AmenityDescription="Proximity to a sandy beach.",
                        AmenityLogo="/images/amenity/Beach.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName="Fireplace",
                        AmenityDescription="A cozy fireplace to warm up.",
                        AmenityLogo="/images/amenity/Fireplace.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName="Gym",
                        AmenityDescription="Fitness area with exercise equipment.",
                        AmenityLogo="/images/amenity/Gym.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName="Hairdrier",
                        AmenityDescription="A device to dry and style hair.",
                        AmenityLogo="/images/amenity/Hairdrier.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName="Ironing",
                        AmenityDescription="Iron and board for clothes pressing.",
                        AmenityLogo="/images/amenity/Ironing.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName="Kitchen",
                        AmenityDescription="A fully equipped kitchen for cooking.",
                        AmenityLogo="/images/amenity/Kitchen.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName="ParkingPlace",
                        AmenityDescription="Dedicated space for vehicle parking.",
                        AmenityLogo="/images/amenity/ParkingPlace.svg"
                    },
                    new AmenityModel
                    {
                        AmenityName="Pool",
                        AmenityDescription="A pool for swimming and relaxation.",
                        AmenityLogo="/images/amenity/Pool.svg"
                    },
                    new AmenityModel
                    {
                        AmenityName="TV",
                        AmenityDescription="Entertainment system with multiple channels.",
                        AmenityLogo="/images/amenity/TV.svg"
                    },
                    new AmenityModel
                    {
                        AmenityName="Waching Machine", // Might want to change this to "WashingMachine"
                        AmenityDescription="A machine for cleaning clothes.",
                        AmenityLogo="/images/amenity/WachingMachine.svg"
                    },
                    new AmenityModel
                    {
                        AmenityName="Wifi",
                        AmenityDescription="Wireless internet connectivity.",
                        AmenityLogo="/images/amenity/Wifi.svg"
                    },
                };
                    context.AddRange(amenities);
                    context.SaveChanges();
            }


            //City
            /*
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
            }*/

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


            /*

            if (!context.Rentals.Any())
            {
                var tvAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/TV.svg");
                var poolAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Pool.svg");
                var wifiAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Wifi.svg");
                var kitchenAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Kitchen.svg");
                var beachAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Beach.svg");
                var gymAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Gym.svg");
                var user = userManager.FindByName("imran.jobb@gmail.com");
                var listnings = new List<ListningModel>
                
                {

                    new ListningModel
                    {
                        User = user,
                        ListningName = "Sentrum Leilighet",
                        ListningDescription = "Moderne leilighet i Oslo sentrum med flott utsikt over byen.",

                        NoOfBeds = 2,
                        SquareMeter = 75,
                        Rating = 4.5f,
                        ListningAddress = "Osloveien 123, 0456 Oslo",
                        ListningPrice = 2000,
                        fromDate = DateOnly.FromDateTime(DateTime.Today),
                        toDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
                        ListningImageURL = "/images/rentals/rental2.png",
                        Amenities = new List<AmenityModel> { tvAmenity, poolAmenity, wifiAmenity } // Add the amenities to the listing
                    }
                    };
                    context.AddRange(listnings);
                    context.SaveChanges();
               }
            */
            

        }
    }
}


