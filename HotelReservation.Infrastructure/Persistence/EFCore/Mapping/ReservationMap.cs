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
    public class ReservationMap : BaseMap<Reservation>
    {
        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {
            base.Configure(builder);

            builder.ToTable("Reservation");

            builder.Property(res => res.CheckInDate)
                .IsRequired();

            builder.Property(res => res.CheckOutDate)
                .IsRequired();

            builder.Property(res => res.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(res => res.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(res => res.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(res => res.Room)
                .WithMany(q=>q.Reservations)
                .HasForeignKey(res => res.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
