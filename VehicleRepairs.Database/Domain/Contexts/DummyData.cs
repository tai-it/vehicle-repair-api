namespace VehicleRepairs.Database.Domain.Contexts
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VehicleRepairs.Database.Domain.Entities;
    using VehicleRepairs.Shared.Common;

    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext db, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            db.Database.EnsureCreated();
            await InitRoles(roleManager);
            await InitUsers(userManager);
            await InitStations(db);
        }

        public static async Task InitRoles(RoleManager<Role> roleManager)
        {
            if (await roleManager.FindByNameAsync(CommonConstants.Roles.SUPER_ADMIN) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = CommonConstants.Roles.SUPER_ADMIN });
            }
            if (await roleManager.FindByNameAsync(CommonConstants.Roles.ADMIN) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = CommonConstants.Roles.ADMIN });
            }
            if (await roleManager.FindByNameAsync(CommonConstants.Roles.STATION) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = CommonConstants.Roles.STATION });
            }
            if (await roleManager.FindByNameAsync(CommonConstants.Roles.USER) == null)
            {
                await roleManager.CreateAsync(new Role() { Name = CommonConstants.Roles.USER });
            }
        }

        private static async Task InitUsers(UserManager<User> userManager)
        {
            string password = "P@$$w0rd";

            // SUPER ADMIN
            if (await userManager.FindByNameAsync("0987654321") == null)
            {
                var user = new User
                {
                    Name = "Đặng Thành Tài",
                    UserName = "0987654321",
                    Email = "super.admin@suaxe.com",
                    PhoneNumber = "0987654321",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.SUPER_ADMIN);
                }
            }

            // ADMIN
            if (await userManager.FindByNameAsync("0987654322") == null)
            {
                var user = new User
                {
                    Name = "Đặng Phương Nam",
                    UserName = "0987654322",
                    Email = "admin@suaxe.com",
                    PhoneNumber = "0987654322",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.ADMIN);
                }
            }

            // STATIONS
            if (await userManager.FindByNameAsync("0987654323") == null)
            {
                var user = new User
                {
                    Name = "Nguyễn Văn Thìn",
                    UserName = "0987654323",
                    Email = "thin.nguyen@gmail.com",
                    PhoneNumber = "0987654323",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654324") == null)
            {
                var user = new User
                {
                    Name = "Trần Bá Thiên",
                    UserName = "0987654324",
                    Email = "thien.tran@gmail.com",
                    PhoneNumber = "0987654324",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654325") == null)
            {
                var user = new User
                {
                    Name = "Hoàng Thiên Phước",
                    UserName = "0987654325",
                    Email = "phuoc.hoang@gmail.com",
                    PhoneNumber = "0987654325",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654326") == null)
            {
                var user = new User
                {
                    Name = "Nguyễn Hoàng Gia",
                    UserName = "0987654326",
                    Email = "gia.hoang@gmail.com",
                    PhoneNumber = "0987654326",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654327") == null)
            {
                var user = new User
                {
                    Name = "Phan Thanh Hùng",
                    UserName = "0987654327",
                    Email = "hung.phan@gmail.com",
                    PhoneNumber = "0987654327",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }
        }

        private static async Task InitStations(ApplicationDbContext db)
        {
            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654323") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654323");

                var station = new Station()
                {
                    User = owner,
                    Name = "Thìn Yamaha",
                    Address = "224 Đường 2/9, Hải Châu, Đà Nẵng",
                    Latitude = (decimal)16.06778,
                    Longitude = (decimal)108.22083,
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 8,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 65000
                        },
                        new Service()
                        {
                            Name = "Thay lọc máy",
                            Price = 350000
                        },
                        new Service()
                        {
                            Name = "Thay bugi",
                            Price = 180000
                        },
                        new Service()
                        {
                            Name = "Thay nhớt",
                            Price = 160000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 20000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654324") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654324");

                var station = new Station()
                {
                    User = owner,
                    Name = "Thiên Motor",
                    Address = "56 Nguyễn Văn Thoại, Sơn Trà, Đà Nẵng",
                    Latitude = (decimal)16.06068,
                    Longitude = (decimal)108.23256,
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 8,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 120000
                        },
                        new Service()
                        {
                            Name = "Thay lọc máy",
                            Price = 400000
                        },
                        new Service()
                        {
                            Name = "Thay bugi",
                            Price = 250000
                        },
                        new Service()
                        {
                            Name = "Thay nhớt",
                            Price = 160000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 30000
                        },
                        new Service()
                        {
                            Name = "Cắt đuôi",
                            Price = 550000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654325") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654325");

                var station = new Station()
                {
                    User = owner,
                    Name = "Phước Honda",
                    Address = "60 Tiểu La, Hải Châu, Đà Nẵng",
                    Latitude = (decimal)16.0444305,
                    Longitude = (decimal)108.2134076,
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = false,
                    Coefficient = 0,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 65000
                        },
                        new Service()
                        {
                            Name = "Thay nhớt",
                            Price = 165000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 30000
                        },
                        new Service()
                        {
                            Name = "Thay săm",
                            Price = 130000
                        },
                        new Service()
                        {
                            Name = "Thay lốp",
                            Price = 220000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654326") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654326");

                var station = new Station()
                {
                    User = owner,
                    Name = "H.G Garage",
                    Address = "80 Võ Văn Kiệt, Sơn Trà, Đà Nẵng",
                    Latitude = (decimal)16.0627823,
                    Longitude = (decimal)108.2410708,
                    Vehicle = CommonConstants.Vehicles.CAR,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 12,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 2000000
                        },
                        new Service()
                        {
                            Name = "Dán decal",
                            Price = 1600000
                        },
                        new Service()
                        {
                            Name = "Làm lốp",
                            Price = 1500000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 200000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654327") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654327");

                var station = new Station()
                {
                    User = owner,
                    Name = "Hùng Garage",
                    Address = "200 Nguyễn Hữu Thọ, Hải Châu, Đà Nẵng",
                    Latitude = (decimal)16.06778,
                    Longitude = (decimal)108.22083,
                    Vehicle = CommonConstants.Vehicles.CAR,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 12,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 2100000
                        },
                        new Service()
                        {
                            Name = "Dán decal",
                            Price = 1500000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 250000
                        },
                        new Service()
                        {
                            Name = "Thay lốp",
                            Price = 800000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            await db.SaveChangesAsync();
        }
    }
}
