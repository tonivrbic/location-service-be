using System;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using LocationServicesApi.Models;

namespace LocationServicesApi
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {
            context.Database.EnsureCreated();

            // Seed if there are no devices
            if (!context.Devices.Any())
            {
                var devicesString = File.ReadAllText("DbData/Devices.json");
                var devices = JsonConvert.DeserializeObject<Device[]>(devicesString);
                context.Devices.AddRange(devices);
                context.SaveChanges();
            }

            // Seed if there are no locations
            if (!context.Locations.Any())
            {
                var locationsString = File.ReadAllText("DbData/Locations.json");
                var locations = JsonConvert.DeserializeObject<Location[]>(locationsString);
                context.Locations.AddRange(locations);
                context.SaveChanges();
            }

        }
    }
}