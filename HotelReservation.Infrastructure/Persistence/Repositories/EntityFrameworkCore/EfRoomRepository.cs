using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Infrastructure.Persistence.Repositories.EntityFrameworkCore.RepositoryAndUnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infrastructure.Persistence.Repositories.EntityFrameworkCore
{
    public class EfRoomRepository : EfRepository<Room>, IRoomRepository
    {
        public EfRoomRepository(DbContext context) : base(context)
        {
        }
    }
}
