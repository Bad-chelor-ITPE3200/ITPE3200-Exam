
using FastFlat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FastFlat.DAL
{
    public class DBInit
    {
        public static async Task Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            RentalDbContext context = serviceScope.ServiceProvider.GetRequiredService<RentalDbContext>();
            var userman = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(); //usermanager
            var roleman = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(); //rolemanager
            var hostingprovider =
                serviceScope.ServiceProvider.GetRequiredService<IWebHostEnvironment>(); //Hostingprovider

            //Paths for pbs

            //Oli PB
            byte[] oliarr; //declears the array
            using (var filestreamOli =
                   new FileStream(Path.Combine(hostingprovider.WebRootPath, "images", "profilepicture", "oliver.jpg"),
                       FileMode.Open, FileAccess.Read)) //filestream to "load" in the profile pictures
            {
                using var
                    oliarrReader =
                        new BinaryReader(filestreamOli); //converting the filestream of the opend file to binary
                oliarr = oliarrReader.ReadBytes((int)filestreamOli
                    .Length); //reading the bytes and turning them to a arrays byte array
            }


            //ali PB
            byte[] aliarr;
            using (var filestreamAli =
                   new FileStream(Path.Combine(hostingprovider.WebRootPath, "images", "profilepicture", "ali.jpg"),
                       FileMode.Open, FileAccess.Read))
            {
                using var aliarrReader = new BinaryReader(filestreamAli);
                aliarr = aliarrReader.ReadBytes((int)filestreamAli.Length);
            }

            //JP 
            byte[] jparr;
            using (var filestreamJp =
                   new FileStream(Path.Combine(hostingprovider.WebRootPath, "images", "profilepicture", "jp.jpg"),
                       FileMode.Open, FileAccess.Read))
            {
                using var jparrReader = new BinaryReader(filestreamJp);
                jparr = jparrReader.ReadBytes((int)filestreamJp.Length);
            }

            //Gistrong PB
            byte[] gislearr;
            using (var filestreamGisle =
                   new FileStream(Path.Combine(hostingprovider.WebRootPath, "images", "profilepicture", "gisle.jpg"),
                       FileMode.Open, FileAccess.Read))
            {
                var gilsearrReader = new BinaryReader(filestreamGisle);
                gislearr = gilsearrReader.ReadBytes((int)filestreamGisle.Length);
            }

            //Creation/deletion of the database: 
            context.Database.EnsureCreated();
            //context.Database.EnsureDeleted();  // ready if we want to use it
            //Amenity

            if (!context.Amenities.Any()) //if the context is empty
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
                await context.AddRangeAsync(amenities);
                await context.SaveChangesAsync();
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
                        Contryname = "Norway"
                    },

                    new ContryModel
                    {
                        Contryname = "Sweden"
                    },

                    new ContryModel
                    {
                        Contryname = "Denmark"
                    },
                };
                await context.AddRangeAsync(country);
                await context.SaveChangesAsync();
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
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser()
                    {
                        UserName = "Olidrav",
                        FirstName = "Oliver",
                        LastName = "Dragland",
                        Email = "oliver@gmail.com",
                        PhoneNumber = "99999999",
                        ProfilePicture = oliarr //the array for earlier
                    },

                    new ApplicationUser
                    {
                        UserName = "jp@gmail.com",
                        FirstName = "Jon",
                        LastName = "Petter",
                        Email = "jp@gmail.com",
                        PhoneNumber = "9988888",
                        ProfilePicture = jparr
                    },

                    new ApplicationUser
                    {
                        UserName = "Gistrong",
                        FirstName = "Gisle",
                        LastName = "Na",
                        Email = "Gisle@gmail.com",
                        PhoneNumber = "99777777",
                        ProfilePicture = gislearr
                    },

                    new ApplicationUser
                    {
                        UserName = "Alinam",
                        FirstName = "Ali",
                        LastName = "Anjum",
                        Email = "Ali@gmail.com",
                        //PassWord = "Password123!",
                        // PasswordHash = passwordhasher.HashPassword(null, "Password123!"),
                        PhoneNumber = "99666666",
                        ProfilePicture = aliarr
                        //Rentals = new List<ListningModel> { },
                        //Bookings = new List<BookingModel> { },
                    }
                };
                try
                {
                    foreach (ApplicationUser u in users) //for loop for all the users
                    {
                        var ok = userman.CreateAsync(u, "2?7E'AbTy96?vC@")
                            .Result; //a check if the account is created or not
                        Console.Write(ok);
                        if (ok.Succeeded) //if it is allright
                        {
                            Console.Write(u.NormalizedEmail + "is created at: " +
                                          DateTime.Now); //writes where is was created
                            await userman.AddToRoleAsync(u, "Admin"); //adds to the admin role
                        }
                        else
                        {
                            Console.Write("error in creating user " + u); //if there is a error 
                        }
                    }
                }
                catch (Exception ex) // throws exeption if there is a problem 
                {
                    Console.WriteLine(ex);
                }
            }


        }
    }
}
//TODO: Vi bør vurdere om vi skal ha bookings i initdb, tror det kan bli mye krøll (spess mye kræsj hvis vi shipper med database, så kan være lurt å ha det i INITDB isteden, er samme funksjon, men vi slipper en del struggle)

