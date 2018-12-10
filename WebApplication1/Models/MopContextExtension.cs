using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.User;

namespace WebApplication1.Models
{
    public static class MopContextExtension
    {
        const int DeviceNo = 5;
        const int TempPerDevice = 100;
        
        public static void EnsureSeedData(this MopContext context)
        {
            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.Devices.Any())
                {
                    //Create 5 devices
                    var devices = new List<Device>();
                    for (int i = 0; i < DeviceNo; i++)
                    {
                        devices.Add(
                            new Faker<Device>()
                            .RuleFor(o => o.Name, f => f.Hacker.Noun())
                        );
                    }
                    
                    context.Devices.AddRange(devices);
                    context.SaveChanges();
                }

                if (!context.Temperatures.Any())
                {
                    //Create `TempPerDevice` entires for each device
                    foreach (var device in context.Devices)
                    {
                        var now = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        var beginning = now - 60 * 60 * 24 * 365;
                        var timeInterval = (int)Math.Floor((double)(now - beginning) / TempPerDevice);

                        if (timeInterval == 0) timeInterval = 1;
                        
                        for (int i = 0; i < TempPerDevice; i++)
                        {
                            context.Temperatures.Add(
                                new Temperature
                                {
                                    Temp = Math.Round(new Faker().Random.Double() * 50 + 50, 2),
                                    Timestamp = beginning,
                                    Device = device
                                }
                            );

                            beginning += timeInterval;
                        }
                    }
                    
                    context.SaveChanges();
                }
                
                if (!context.Roles.Any())
                {
                    context.Roles.Add(new Role()
                    {
                        Name = "admin"
                    });
                    context.Roles.Add(new Role()
                    {
                        Name = "subscriber"
                    });
                    context.SaveChanges();
                }
                
                if (!context.Users.Any())
                {
                    var admin = new User.User()
                    {
                        Forename = "Administrator", Email = "admin@site.com", Username = "admin", Password = "dupa8"
                    };
                    context.Users.Add(admin);
                    
                    var role = context.Roles.ToList().Find(x => x.Name == "admin");
                    if (role != null)
                    {
                        admin.UserRoles.Add(new UserRole() {UserId = admin.Id, Role = role});
                    }
                    context.SaveChanges();

                    var subscriber = new User.User()
                    {
                        Forename = "Johny", Surname = "Walker", Email = "johny@site.com", Username = "johny", Password = "dupa9"
                    };
                    context.Users.Add(subscriber);
                    
                    var role2 = context.Roles.ToList().Find(x => x.Name == "subscriber");
                    if (role2 != null)
                    {
                        admin.UserRoles.Add(new UserRole() {UserId = subscriber.Id, Role = role2});
                    }
                    
                    context.SaveChanges();
                }
            }
        }
    }
}