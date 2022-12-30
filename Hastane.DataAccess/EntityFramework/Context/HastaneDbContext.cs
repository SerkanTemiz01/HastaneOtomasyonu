﻿using Hastane.DataAccess.EntityFramework.Mapping;
using Hastane.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane.DataAccess.EntityFramework.Context
{
    public class HastaneDbContext:DbContext
    {
        public HastaneDbContext(DbContextOptions<HastaneDbContext> options):base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Personel> Personels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdminMapping())
                .ApplyConfiguration(new ManagerMapping())
                .ApplyConfiguration(new PersonelMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
