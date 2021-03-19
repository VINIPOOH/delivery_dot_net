﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DAL.Entity;
 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

 namespace WebApplication1.Dal
{
    public class MyDbContext : IdentityDbContext
    {
        public DbSet<Way> Ways { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<TariffWeightFactor> TariffWeightFactors { get; set; }
        public DbSet<WayToTariffWeightFactor> WayToTariffWeightFactors { get; set; }

        public MyDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conectionString = "server=localhost;database=labweb;uid=root;pwd=root;";
            optionsBuilder.UseMySql(conectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasMany(u=>u.Bills).WithOne(e => e.User).IsRequired();
            builder.Entity<User>().HasMany(u=>u.Deliveries).WithOne(e => e.Addressee).IsRequired();
            builder.Entity<Way>().HasMany(u=>u.Deliveries).WithOne(e => e.Way).IsRequired();
            builder.Entity<Bill>().HasOne(u=>u.Delivery).WithOne(e => e.Bill).IsRequired();
            builder.Entity<Locality>().HasMany(u=>u.WaysWhereThisLocalityIsGet).WithOne(e => e.LocalityGet).IsRequired();
            builder.Entity<Locality>().HasMany(u=>u.WaysWhereThisLocalityIsSend).WithOne(e => e.LocalitySand).IsRequired();
            builder.Entity<Way>().HasMany(u=>u.WayToTariffWeightFactors).WithOne(e => e.Way).IsRequired();
            builder.Entity<TariffWeightFactor>().HasMany(u=>u.WayToTariffWeightFactors).WithOne(e => e.TariffWeightFactor).IsRequired();
        }
    }
}