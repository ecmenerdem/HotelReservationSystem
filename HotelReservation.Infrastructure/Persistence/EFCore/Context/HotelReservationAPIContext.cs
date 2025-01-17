﻿using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.Persistence.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infrastructure.Persistence.EFCore.Context
{
    public class HotelReservationAPIContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("data source=EERDEM\\SQLEXPRESS;initial catalog=HotelReservationDB;integrated security=True; TrustServerCertificate=true");

                optionsBuilder.UseSqlServer("server=.;Database=HotelReservationDB;Integrated Security=true; TrustServerCertificate=true");
            }
            //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")=="Development")
            //{
            //    optionsBuilder.UseSqlServer("data source=.\\ZRVSQL2008;initial catalog=ShoppingDB;integrated security=True; TrustServerCertificate=true");
            //}

            //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            //{
            //    optionsBuilder.UseSqlServer("data source=.\\ZRVSQL2008;initial catalog=ShoppingDB;integrated security=True; TrustServerCertificate=true");
            //}


            base.OnConfiguring(optionsBuilder);
        }

        // DbSet'ler: Veritabanında tabloları temsil eder.
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RoomImage> RoomImages { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapping sınıflarını burada çağırıyoruz.
            modelBuilder.ApplyConfiguration(new HotelMap());
            modelBuilder.ApplyConfiguration(new RoomMap());
            modelBuilder.ApplyConfiguration(new ReservationMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoomImageMap());
            modelBuilder.ApplyConfiguration(new UserGroupMap());

        }
    }
}