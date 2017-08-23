using LocationServicesApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationServicesApi
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
    }
}
