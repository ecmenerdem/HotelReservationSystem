using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Repositories.DataManagement
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IHotelRepository HotelRepository { get; }
        IRoomRepository RoomRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IRoomImageRepository RoomImageRepository { get; }

        Task<int> SaveAsync();
    }
}
