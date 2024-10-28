using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Infrastructure.Persistence.Repositories.EntityFrameworkCore.RepositoryAndUnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infrastructure.Persistence.Repositories.EntityFrameworkCore
{
    public class EfRoomImageRepository : EfRepository<RoomImage>, IRoomImageRepository
    {
        public EfRoomImageRepository(DbContext context) : base(context)
        {
        }
    }
}
