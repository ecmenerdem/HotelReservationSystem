using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Infrastructure.Persistence.Repositories.EntityFrameworkCore.RepositoryAndUnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infrastructure.Persistence.Repositories.EntityFrameworkCore
{
    public class EfUserGroupRepository : EfRepository<UserGroup>,IUserGroupRepository
    {
        public EfUserGroupRepository(DbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
