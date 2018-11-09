using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public static class MopContextExtension
    {
        const int DeviceNo = 5;
        const int TempPerDevice = 100000;
        
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
            }
        }
    }
}