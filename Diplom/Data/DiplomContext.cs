﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;

namespace Diplom.Data
{
    public class DiplomContext : DbContext
    {
        public DiplomContext (DbContextOptions<DiplomContext> options)
            : base(options)
        {
        }

        public DbSet<Diplom.Models.Device> Device { get; set; }
        public DbSet<Diplom.Models.Placement> Placement { get; set; } 
        public DbSet<Diplom.Models.DevicePlacement> DevicePlacement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<DevicePlacement>()
                .HasKey(c => new { c.DeviceID, c.PlacementID });
        }
    }
}
