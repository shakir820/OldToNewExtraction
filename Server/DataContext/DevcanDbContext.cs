using Microsoft.EntityFrameworkCore;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DataContext
{
    public class DevcanDbContext:DbContext
    {
        public DevcanDbContext(DbContextOptions<DevcanDbContext> options)
          : base(options)
        {

        }
        public DbSet<OldImageObj> OldImages { get; set; }
        public DbSet<ImageObj> NewImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OldImageObj>().ToTable("old");
            modelBuilder.Entity<ImageObj>().ToTable("new");
        }
    }
}
