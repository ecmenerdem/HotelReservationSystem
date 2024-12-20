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
        Task<ApiResult<HotelDTO>> AddHotelAsync(HotelAddRequestDTO hotelDto);
        Task<ApiResult<bool>> UpdateHotelAsync(HotelUpdateRequestDTO hotelDto);
        Task<ApiResult<bool>> DeleteHotelAsync(int hotelId);
    }
}
