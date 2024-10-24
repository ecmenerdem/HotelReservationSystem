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
    public class RoomImageMap : BaseMap<RoomImage>
    {
        public override void Configure(EntityTypeBuilder<RoomImage> builder)
        {
            base.Configure(builder);

            builder.ToTable("RoomImage");

            builder.Property(ri => ri.ImageUrl)
                .IsRequired();

            builder.HasOne(ri => ri.Room)
                .WithMany(r => r.RoomImages)
                .HasForeignKey(ri => ri.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
