using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Repositories.DataManagement;

namespace HotelReservation.Domain.Repositories
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<Hotel> GetHotelWithRoomsAsync(int hotelId);
    }

}
