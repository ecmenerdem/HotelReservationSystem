﻿using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Infrastructure.Persistence.Repositories.EntityFrameworkCore.RepositoryAndUnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class EfRoomRepository : EfRepository<Room>, IRoomRepository
    {
        public EfRoomRepository(DbContext context) : base(context)
        {
        }
    }
}