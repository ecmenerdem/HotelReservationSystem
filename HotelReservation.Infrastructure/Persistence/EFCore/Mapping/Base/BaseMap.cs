﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Domain.Entities.Base;

namespace HotelReservation.Infrastructure.Persistence.EFCore.Mapping.Base
{
    public class BaseMap<T> : IEntityTypeConfiguration<T> where T : AuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(q => q.ID);
            builder.Property(q => q.GUID).ValueGeneratedOnAdd();
            builder.Property(q => q.ID).ValueGeneratedOnAdd();

            // AuditableEntity'den gelen ortak alanlar için konfigurasyon
            builder.Property(q => q.IsActive).HasDefaultValue(true);
            builder.Property(q => q.IsDeleted).HasDefaultValue(false);
         
        }
    }
}
