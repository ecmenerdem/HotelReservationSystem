using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Infrastructure.Persistence.EFCore.Mapping.Base;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Persistence.EFCore.Mapping
{
    public class HotelMap : BaseMap<Hotel>
    {
        public override void Configure(EntityTypeBuilder<Hotel> builder)
        {
            base.Configure(builder); // BaseMap'deki temel konfigurasyonları uygula

            builder.ToTable("Hotel");

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.Address)
                .IsRequired()
                .HasMaxLength(500);
            
            //builder.HasQueryFilter(q=>q.IsActive==true);  TODO: HasQuery Filter Kullanımı

            // Hotel - Room ilişkisi
            builder.HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
