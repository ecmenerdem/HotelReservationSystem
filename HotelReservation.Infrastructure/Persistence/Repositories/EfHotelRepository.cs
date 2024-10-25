using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories;
using HotelReservation.Infrastructure.Persistence.EFCore.Context;
using HotelReservation.Infrastructure.Persistence.Repositories.EntityFrameworkCore.RepositoryAndUnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infrastructure.Persistence.Repositories
{
    public class EfHotelRepository : EfRepository<Hotel>, IHotelRepository
    {
        private readonly DbContext _context;

        public EfHotelRepository(DbContext context) : base(context)
        {
            _context = context;
        }

       

        // Hotel'e özel ek sorgular veya işlemler buraya eklenebilir
        public async Task<Hotel> GetHotelWithRoomsAsync(int hotelId)
        {
            return await _context.Set<Hotel>().Include(h => h.Rooms).SingleOrDefaultAsync(h => h.ID == hotelId);
        }
    }
}
