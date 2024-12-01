using HotelReservation.Application.DTO.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Application.Result;

namespace HotelReservation.Application.Contracts.Persistence
{
    public interface IHotelService
    {
        Task<ApiResult<HotelDTO>> GetHotelByIdAsync(int hotelId);
        Task<ApiResult<IEnumerable<HotelDTO>>> GetAllHotelsAsync();
        Task<HotelDTO> AddHotelAsync(HotelAddRequestDTO hotelDto);
        Task<bool> UpdateHotelAsync(HotelUpdateRequestDTO hotelDto);
        Task<bool> DeleteHotelAsync(int hotelId);
    }
}
