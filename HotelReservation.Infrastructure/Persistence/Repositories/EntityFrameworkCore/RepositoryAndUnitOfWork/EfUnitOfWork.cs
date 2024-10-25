using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Entities.Base;
using HotelReservation.Domain.Repositories;
using HotelReservation.Domain.Repositories.DataManagement;
using HotelReservation.Infrastructure.Persistence.EFCore.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Infrastructure.Persistence.Repositories.EntityFrameworkCore.RepositoryAndUnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly HotelReservationAPIContext _context;
        private readonly IHttpContextAccessor _accessor;

        public EfUnitOfWork(HotelReservationAPIContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            HotelRepository = new EfHotelRepository(_context);
            RoomRepository = new EfRoomRepository(_context);
            ReservationRepository = new EfReservationRepository(_context);
            UserRepository = new EfUserRepository(_context);
            RoomImageRepository = new EfRoomImageRepository(_context);
            _accessor = accessor;
        }

        public IUserRepository UserRepository { get; }
      public IHotelRepository HotelRepository { get; }
      public IRoomRepository RoomRepository { get; }
      public IReservationRepository ReservationRepository { get; }
      public IRoomImageRepository RoomImageRepository { get; }

        public async Task<int> SaveAsync()
        {
            foreach (EntityEntry<AuditableEntity> item in _context.ChangeTracker.Entries<AuditableEntity>())
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.AddedTime = DateTime.Now;
                    item.Entity.UpdateTime = DateTime.Now;
                    item.Entity.AddedUser = (int?) _accessor?.HttpContext?.Items["HotelResevationUIUserID"];
                    item.Entity.AddedIP = _accessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                }
                else if (item.State == EntityState.Modified)
                {
                    item.Entity.UpdateTime = DateTime.Now;
                    item.Entity.UpdateUser = (int?)_accessor?.HttpContext?.Items["HotelResevationUIUserID"];
                    item.Entity.UpdatedIP = _accessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                }
            }

            int CountOfData = await _context.SaveChangesAsync();
            return CountOfData;
        }
    }
}
