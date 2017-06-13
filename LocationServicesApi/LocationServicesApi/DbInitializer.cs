using System;
using System.Linq;

namespace LocationServicesApi
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Devices.Any())
            {
                return;   // DB has been seeded
            }

            context.SaveChanges();
        }
    }
}