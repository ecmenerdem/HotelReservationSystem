using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.Persistence.EFCore.Mapping.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infrastructure.Persistence.EFCore.Mapping
{
    public class RoomMap : BaseMap<Room>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.ToTable("Room");

            builder.Property(r => r.RoomType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Capacity)
                .IsRequired();

            builder.Property(r => r.PricePerNight)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(r => r.IsAvailable)
                .IsRequired();

            //// Room - Reservation ilişkisi
            //builder.HasMany(r => r.Reservations)
            //    .WithOne(res => res.Room)
            //    .HasForeignKey(res => res.RoomId)
            //    .OnDelete(DeleteBehavior.Cascade);

            // Room - RoomImage ilişkisi
            builder.HasMany(r => r.RoomImages)
                .WithOne(ri => ri.Room)
                .HasForeignKey(ri => ri.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
