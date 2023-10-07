using FastFlat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.DAL
{
    public class DBInit
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RentalDbContext _context;

        public DBInit(UserManager<IdentityUser> userManager, RentalDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task Seed()
        {
            //using var serviceScope = app.ApplicationServices.CreateScope();
            //RentalDbContext context = serviceScope.ServiceProvider.GetRequiredService<RentalDbContext>();
            //_context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            //usermanager

            /*

            if (!_userManager.Users.Any())
            {
                var users = new List<IdentityUser>
    {
        new IdentityUser
        {
            UserName = "oliver",
            Email = "oliver@gmail.com",
            PhoneNumber = "99666666",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Password123!"),
            //SecurityStamp = "/images/profilepicture/oliver.jpg"
            // Remember: SecurityStamp is NOT for images. It's a security feature.
            //SecurityStamp = Guid.NewGuid().ToString()
        },
        new IdentityUser
        {
            UserName = "ali.jobb@live.com", // Must be unique
            Email = "ali.jobb@live.com",    // Must be unique
            PhoneNumber = "99666667",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Password123!"),
            //SecurityStamp = "/images/profilepicture/ali.jpg"
            //SecurityStamp = Guid.NewGuid().ToString()
        }
    };

                foreach (var user in users)
                {
                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        // Handle errors or log them.
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                    }
                }
            }
            */


            //Amenity
            if (!_context.Amenities.Any())
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

                     new AmenityModel
                    {
                        AmenityName="Fjert",
                        AmenityDescription="Wireless internet connectivity.",
                        AmenityLogo="/images/amenity/Wifi.svg"
                    },
                };
                    _context.AddRange(amenities);
                    _context.SaveChanges();
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
            if (!_context.Countries.Any())
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
                _context.AddRange(country);
                _context.SaveChanges();
            }



            if (!_context.Users.Any())
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
                _context.AddRange(users);
                _context.SaveChanges();
            }


            

            if (!_context.Rentals.Any())
            {
                var tvAmenity = _context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/TV.svg");
                var poolAmenity = _context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Pool.svg");
                var wifiAmenity = _context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Wifi.svg");
                var kitchenAmenity = _context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Kitchen.svg");
                var beachAmenity = _context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Beach.svg");
                var gymAmenity = _context.Amenities.FirstOrDefault(a => a.AmenityLogo == "/images/amenity/Gym.svg");
                var userEmail1 = "ali.jobb@live.com"; // replace with a real email78 from your DB
                var userEmail2 = "imran.jobb@gmail.com";
                var user1 = _userManager.FindByEmailAsync(userEmail1).Result;
                var user2 = _userManager.FindByEmailAsync(userEmail2).Result;

                
                var listnings = new List<ListningModel>
                
                {

                    new ListningModel
                    {
                        User = user1,
                        ListningName = "Kylling Hotellet ",
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
                    },
                    
                    new ListningModel
                    {
                        User = user2,
                        ListningName = "GateKjøkken Leilighet",
                        ListningDescription = "Rett ved stranda.",
                        NoOfBeds = 2,
                        SquareMeter = 75,
                        Rating = 4.5f,
                        ListningAddress = "Osloveien 123, 0456 Oslo",
                        ListningPrice = 2000,
                        fromDate = DateOnly.FromDateTime(DateTime.Today),
                        toDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
                        ListningImageURL = "/images/rentals/rental1.png",
                        Amenities = new List<AmenityModel> { kitchenAmenity, gymAmenity, wifiAmenity } // Add the amenities to the listing
                    }
                    
                };
                _context.AddRange(listnings);
                _context.SaveChanges();

                
            
            }
                       
        }
    }
}


