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

            if (await userManager.FindByNameAsync("0987654328") == null)
            {
                var user = new User
                {
                    Name = "Nguyễn Thành Công",
                    UserName = "0987654328",
                    Email = "cong.nguyen@gmail.com",
                    PhoneNumber = "0987654328",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654329") == null)
            {
                var user = new User
                {
                    Name = "Nguyễn Hoàng Phúc",
                    UserName = "0987654329",
                    Email = "phuc.nguyen@gmail.com",
                    PhoneNumber = "0987654329",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654310") == null)
            {
                var user = new User
                {
                    Name = "Đinh Như Luân",
                    UserName = "0987654310",
                    Email = "luan.dinh@gmail.com",
                    PhoneNumber = "0987654310",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654311") == null)
            {
                var user = new User
                {
                    Name = "Phùng Thanh Độ",
                    UserName = "0987654311",
                    Email = "mixi.phung@gmail.com",
                    PhoneNumber = "0987654311",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654312") == null)
            {
                var user = new User
                {
                    Name = "Trần Mạnh Tuấn",
                    UserName = "0987654312",
                    Email = "tuan.tran@gmail.com",
                    PhoneNumber = "0987654312",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654313") == null)
            {
                var user = new User
                {
                    Name = "Nguyễn Trường Giang",
                    UserName = "0987654313",
                    Email = "giang.nguyen@gmail.com",
                    PhoneNumber = "0987654313",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654314") == null)
            {
                var user = new User
                {
                    Name = "Phan Minh Cường",
                    UserName = "0987654314",
                    Email = "cuong.phan@gmail.com",
                    PhoneNumber = "0987654314",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654315") == null)
            {
                var user = new User
                {
                    Name = "Nguyễn Trần Duy Nhất",
                    UserName = "0987654315",
                    Email = "nhat.nguyen@gmail.com",
                    PhoneNumber = "0987654315",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654316") == null)
            {
                var user = new User
                {
                    Name = "Trần Nhất Nguyên",
                    UserName = "0987654316",
                    Email = "nguyen.tran@gmail.com",
                    PhoneNumber = "0987654316",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654317") == null)
            {
                var user = new User
                {
                    Name = "Trần Quang Thái",
                    UserName = "0987654317",
                    Email = "thai.tran@gmail.com",
                    PhoneNumber = "0987654317",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654318") == null)
            {
                var user = new User
                {
                    Name = "Hoàng Văn Nghĩa",
                    UserName = "0987654318",
                    Email = "nghia.hoang@gmail.com",
                    PhoneNumber = "0987654318",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654319") == null)
            {
                var user = new User
                {
                    Name = "Hồ Văn Huy",
                    UserName = "0987654319",
                    Email = "huy.ho@gmail.com",
                    PhoneNumber = "0987654319",
                    PhoneNumberConfirmed = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, CommonConstants.Roles.STATION);
                }
            }

            if (await userManager.FindByNameAsync("0987654320") == null)
            {
                var user = new User
                {
                    Name = "Nguyễn Thái Hoàng",
                    UserName = "0987654320",
                    Email = "hoang.nguyen@gmail.com",
                    PhoneNumber = "0987654320",
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
            // Motorbikes
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
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 12,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 65000
                        },
                        new Service()
                        {
                            Name = "Dán decal",
                            Price = 500000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 20000
                        },
                        new Service()
                        {
                            Name = "Thay lốp",
                            Price = 180000
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

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654328") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654328");

                var station = new Station()
                {
                    User = owner,
                    Name = "TC Honda",
                    Address = "90 Tô Hiến Thành, Sơn Trà, Đà Nẵng",
                    Latitude = (decimal)16.06068,
                    Longitude = (decimal)108.23256,
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 12,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 65000
                        },
                        new Service()
                        {
                            Name = "Dán decal",
                            Price = 500000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 20000
                        },
                        new Service()
                        {
                            Name = "Thay lốp",
                            Price = 180000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654329") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654329");

                var station = new Station()
                {
                    User = owner,
                    Name = "Phúc Motor",
                    Address = "25 Trần Cao Vân, Hải Châu, Đà Nẵng",
                    Latitude = (decimal)16.06778,
                    Longitude = (decimal)108.22083,
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 12,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 65000
                        },
                        new Service()
                        {
                            Name = "Dán decal",
                            Price = 500000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 20000
                        },
                        new Service()
                        {
                            Name = "Thay lốp",
                            Price = 180000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654310") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654310");

                var station = new Station()
                {
                    User = owner,
                    Name = "Luân Honda",
                    Address = "130 Nguyễn Thị Thập, Thanh Khê, Đà Nẵng",
                    Latitude = (decimal)16.0729982,
                    Longitude = (decimal)108.1746968,
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 12,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 65000
                        },
                        new Service()
                        {
                            Name = "Dán decal",
                            Price = 500000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 20000
                        },
                        new Service()
                        {
                            Name = "Thay lốp",
                            Price = 180000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654311") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654311");

                var station = new Station()
                {
                    User = owner,
                    Name = "Mixi Store",
                    Address = "116 Lê Duẩn, Hải Châu, Đà Nẵng",
                    Latitude = (decimal)16.0717262,
                    Longitude = (decimal)108.2231675,
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 12,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 65000
                        },
                        new Service()
                        {
                            Name = "Dán decal",
                            Price = 500000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 20000
                        },
                        new Service()
                        {
                            Name = "Thay lốp",
                            Price = 180000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654312") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654312");

                var station = new Station()
                {
                    User = owner,
                    Name = "MT Motor",
                    Address = "88 Trưng Nữ Vương, Hải Châu, Đà Nẵng",
                    Latitude = (decimal)16.0456208,
                    Longitude = (decimal)108.2099237,
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 12,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 65000
                        },
                        new Service()
                        {
                            Name = "Dán decal",
                            Price = 500000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 20000
                        },
                        new Service()
                        {
                            Name = "Thay lốp",
                            Price = 180000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654313") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654313");

                var station = new Station()
                {
                    User = owner,
                    Name = "TG Motor",
                    Address = "96 Duy Tân, Hải Châu, Đà Nẵng",
                    Latitude = (decimal)16.0483737,
                    Longitude = (decimal)108.2124706,
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 12,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 65000
                        },
                        new Service()
                        {
                            Name = "Dán decal",
                            Price = 500000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 20000
                        },
                        new Service()
                        {
                            Name = "Thay lốp",
                            Price = 180000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654314") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654314");

                var station = new Station()
                {
                    User = owner,
                    Name = "Mr. Cường Store",
                    Address = "166 Phan Đăng Lưu, Hải Châu, Đà Nẵng",
                    Latitude = (decimal)16.0377394,
                    Longitude = (decimal)108.223675,
                    Vehicle = CommonConstants.Vehicles.MOTORBIKE,
                    IsAvailable = true,
                    HasAmbulatory = true,
                    Coefficient = 12,
                    Services = new List<Service>()
                    {
                        new Service()
                        {
                            Name = "Thay gương",
                            Price = 65000
                        },
                        new Service()
                        {
                            Name = "Dán decal",
                            Price = 500000
                        },
                        new Service()
                        {
                            Name = "Vá xe",
                            Price = 20000
                        },
                        new Service()
                        {
                            Name = "Thay lốp",
                            Price = 180000
                        }
                    }
                };

                db.Stations.Add(station);
            }

            // Cars
            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654315") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654315");

                var station = new Station()
                {
                    User = owner,
                    Name = "Nhất Garage",
                    Address = "07 Đặng Vũ Hỷ, Sơn Trà, Đà Nẵng",
                    Latitude = (decimal)16.06068,
                    Longitude = (decimal)108.23256,
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

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654316") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654316");

                var station = new Station()
                {
                    User = owner,
                    Name = "Nguyên Garage",
                    Address = "222 Ngô Quyền, Sơn Trà, Đà Nẵng",
                    Latitude = (decimal)16.0759776,
                    Longitude = (decimal)108.2310232,
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

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654317") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654317");

                var station = new Station()
                {
                    User = owner,
                    Name = "QT Garage",
                    Address = "18 Võ Văn Kiệt, Sơn Trà, Đà Nẵng",
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

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654318") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654318");

                var station = new Station()
                {
                    User = owner,
                    Name = "Nghĩa Garage",
                    Address = "99 Lê Độ, Thanh Khê, Đà Nẵng",
                    Latitude = (decimal)16.0660162,
                    Longitude = (decimal)108.2021971,
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

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654319") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654319");

                var station = new Station()
                {
                    User = owner,
                    Name = "Huy Mers",
                    Address = "118 Điện Biên Phủ, Thanh Khê, Đà Nẵng",
                    Latitude = (decimal)16.0667234,
                    Longitude = (decimal)108.2004599,
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

            if (await db.Stations.FirstOrDefaultAsync(x => x.User.PhoneNumber == "0987654320") == null)
            {
                var owner = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == "0987654320");

                var station = new Station()
                {
                    User = owner,
                    Name = "TH Garage",
                    Address = "16 Lê Văn Linh, Hải Châu, Đà Nẵng",
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

            await db.SaveChangesAsync();
        }
    }
}