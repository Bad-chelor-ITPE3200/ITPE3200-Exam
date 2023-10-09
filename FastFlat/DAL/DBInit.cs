using System.Security.Claims;
using FastFlat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace FastFlat.DAL
{
    public class DBInit
    {


        public static async Task Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            RentalDbContext context = serviceScope.ServiceProvider.GetRequiredService<RentalDbContext>();
            var userman = serviceScope.ServiceProvider.GetRequiredService<UserManager<AspNetUsers>>();
            var userStore = serviceScope.ServiceProvider.GetRequiredService<UserStore<AspNetUsers>>();
            var loginman = serviceScope.ServiceProvider.GetRequiredService<SignInManager<AspNetUsers>>();
            var roleman = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //Amenity
            if (!context.Amenities.Any())
            {
                var amenities = new List<AmenityModel>
                {
                    new AmenityModel
                    {
                        AmenityName = "Bathtub",
                        AmenityDescription = "A tub to relax in and take a bath.",
                        AmenityLogo = "/images/amenity/Bathtub.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName = "Beach",
                        AmenityDescription = "Proximity to a sandy beach.",
                        AmenityLogo = "/images/amenity/Beach.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName = "Fireplace",
                        AmenityDescription = "A cozy fireplace to warm up.",
                        AmenityLogo = "/images/amenity/Fireplace.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName = "Gym",
                        AmenityDescription = "Fitness area with exercise equipment.",
                        AmenityLogo = "/images/amenity/Gym.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName = "Hairdrier",
                        AmenityDescription = "A device to dry and style hair.",
                        AmenityLogo = "/images/amenity/Hairdrier.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName = "Ironing",
                        AmenityDescription = "Iron and board for clothes pressing.",
                        AmenityLogo = "/images/amenity/Ironing.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName = "Kitchen",
                        AmenityDescription = "A fully equipped kitchen for cooking.",
                        AmenityLogo = "/images/amenity/Kitchen.svg"
                    },

                    new AmenityModel
                    {
                        AmenityName = "ParkingPlace",
                        AmenityDescription = "Dedicated space for vehicle parking.",
                        AmenityLogo = "/images/amenity/ParkingPlace.svg"
                    },
                    new AmenityModel
                    {
                        AmenityName = "Pool",
                        AmenityDescription = "A pool for swimming and relaxation.",
                        AmenityLogo = "/images/amenity/Pool.svg"
                    },
                    new AmenityModel
                    {
                        AmenityName = "TV",
                        AmenityDescription = "Entertainment system with multiple channels.",
                        AmenityLogo = "/images/amenity/TV.svg"
                    },
                    new AmenityModel
                    {
                        AmenityName = "Waching Machine", // Might want to change this to "WashingMachine"
                        AmenityDescription = "A machine for cleaning clothes.",
                        AmenityLogo = "/images/amenity/WachingMachine.svg"
                    },
                    new AmenityModel
                    {
                        AmenityName = "Wifi",
                        AmenityDescription = "Wireless internet connectivity.",
                        AmenityLogo = "/images/amenity/Wifi.svg"
                    },
                };
                context.AddRangeAsync(amenities);
                context.SaveChangesAsync();
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
               context.AddRangeAsync(country);
               context.SaveChangesAsync();
            }

            if (!context.Roles.Any())
                //roles for the acess controll
                //we let the controller control everything about what they can edit or not
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Id = "0", Name = "Admin" //BIG BOSS, can do everything

                    },
                    new IdentityRole
                    {
                        Id = "2", Name = "Renter" //BUY THING
                    }
                };

                foreach (var r in roles)
                {
                    var ok = roleman.CreateAsync(r).Result;
                    Console.Write(ok);
                    if (ok.Succeeded)
                    {
                        Console.Write(r.Name + "is created at: " + DateTime.Now);
                    }
                    else
                    {
                        Console.Write("error in creating Role " + r);
                    }
                }
            }

            if (!context.Users.Any())
            {
                var users = new List<AspNetUsers>
                {

                    new AspNetUsers
                    {
                        UserName = "Olidrav",
                        FirstName = "Oliver",
                        LastName = "Dragland",
                        Email = "oliver@gmail.com",
                        //PassWord = "Password123!",
                        //   PasswordHash = passwordhasher.HashPassword(null, "Password123!"),
                        PhoneNumber = "99999999",
                        ProfilePicture = "/images/profilepicture/oliver.jpg",
                        Rentals = new List<ListningModel> { },
                        Bookings = new List<BookingModel> { },
                    },

                    new AspNetUsers
                    {
                        UserName = "jp@gmail.com",
                        FirstName = "Jon",
                        LastName = "Petter",
                        Email = "jp@gmail.com",
                        //PassWord = "Password123!",
                        //PasswordHash = passwordhasher.HashPassword(null, "Password123!"),
                        PhoneNumber = "9988888",
                        ProfilePicture = "/images/profilepicture/jp.jpg",
                        Rentals = new List<ListningModel> { },
                        Bookings = new List<BookingModel> { },
                    },

                    new AspNetUsers
                    {
                        UserName = "Gistrong",
                        FirstName = "Gisle",
                        LastName = "Na",
                        Email = "Gisle@gmail.com",
                        //PassWord = "Password123!",
                        // PasswordHash = passwordhasher.HashPassword(null, "Password123!"),
                        PhoneNumber = "99777777",
                        ProfilePicture = "/images/profilepicture/gisle.jpg",
                        Rentals = new List<ListningModel> { },
                        Bookings = new List<BookingModel> { },
                    },

                    new AspNetUsers
                    {
                        UserName = "Alinam",
                        FirstName = "Ali",
                        LastName = "Anjum",
                        Email = "Ali@gmail.com",
                        EmailConfirmed = true,
                        //PassWord = "Password123!",
                        // PasswordHash = passwordhasher.HashPassword(null, "Password123!"),
                        PhoneNumber = "99666666",
                        ProfilePicture = "/images/profilepicture/ali.jpg",
                        Rentals = new List<ListningModel> { },
                        Bookings = new List<BookingModel> { },
                    }
                };
                    try
                    {
                        foreach (AspNetUsers u in users)
                        {

                            var ok = userman.CreateAsync(u, "2?7E'AbTy96?vC@").Result;
                            Console.Write(ok);
                            if (ok.Succeeded)
                            {
                                Console.Write(u.NormalizedEmail + "is created at: " + DateTime.Now);
                                loginman.CreateUserPrincipalAsync(u); 
                                userman.AddToRoleAsync(u, "Admin");
                            }
                            else
                            {
                                Console.Write("error in creating user " + u);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex); 
                    }
                    
                }
            if (!context.Rentals.Any())
            {
                var tvAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/TV.svg");
                var poolAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Pool.svg");
                var wifiAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Wifi.svg");
                var kitchenAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Kitchen.svg");
                var beachAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Beach.svg");
                var gymAmenity = context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Gym.svg");
                var listnings = new List<ListningModel>

                {
                    new ListningModel
                    {
                        user = context.Users.FirstOrDefault(u =>
                            u.UserName == "Alinam"), // Linker denne eiendommen til brukeren 'Alinam'
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
                        Amenities = new List<AmenityModel>
                            { tvAmenity, poolAmenity, wifiAmenity } // Add the amenities to the listing
                    },
                    new ListningModel
                    {
                        user = context.Users.FirstOrDefault(u =>
                            u.UserName == "Alinam"), // Linker denne eiendommen til brukeren 'Alinam'
                        ListningName = "Fjellhytte",
                        ListningDescription = "Koselig hytte i fjellet, perfekt for vinterferier.",
                        
                        NoOfBeds = 5,
                        SquareMeter = 100,
                        Rating = 4.8f,
                        ListningAddress = "Fjellveien 567, 1234 Fjellby",
                        ListningPrice = 3000,
                        fromDate = DateOnly.FromDateTime(DateTime.Today),
                        toDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
                        ListningImageURL = "/images/rentals/rental1.png",
                        Amenities = new List<AmenityModel>
                            { gymAmenity, beachAmenity, kitchenAmenity } // Add the amenities to the listing
                    }
                };
                
                context.AddRange(listnings);
                context.SaveChanges();
            }
            
        }
    }
}

