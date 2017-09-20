using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitesApp.Models
{
    public class MemoryContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Observation> Observations { get; set; }

        public MemoryContext(DbContextOptions<MemoryContext> options) : base(options) { }
    }
}
