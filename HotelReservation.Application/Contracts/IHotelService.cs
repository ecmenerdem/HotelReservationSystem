using HotelReservation.Application.DTO.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.Contracts
{
    public interface IHotelService
    {
        Task<HotelDTO> GetHotelByIdAsync(int hotelId);
        Task<IEnumerable<HotelDTO>> GetAllHotelsAsync();
        Task AddHotelAsync(HotelAddRequestDTO hotelDto);
        Task UpdateHotelAsync(HotelUpdateRequestDTO hotelDto);
        Task DeleteHotelAsync(int hotelId);
    } 
}
