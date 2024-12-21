using HotelReservation.Application.DTO.Reservation;
using HotelReservation.Application.DTO.Room;
using HotelReservation.Application.Result;

namespace HotelReservation.Application.Contracts.Persistence
{
    public interface IReservationService
    {
        Task<ApiResult<IEnumerable<RoomDTO>>> GetAvailableRoomsAsync(Guid hotelGuid, DateTime startDate, DateTime endDate);
        Task<ApiResult<ReservationDTO>> GetReservationByGuidAsync(Guid reservationGuid);
        Task<ApiResult<IEnumerable<ReservationDTO>>> GetAllReservationsAsync();
        Task<ApiResult<IEnumerable<ReservationDTO>>> GetReservationsByUserAsync(Guid userGuid);
        Task<ApiResult<IEnumerable<ReservationDTO>>> GetReservationsByRoomAsync(Guid roomGuid);
        Task<ApiResult<ReservationDTO>> AddReservationAsync(ReservationAddRequestDTO reservationDto);
        Task<ApiResult<bool>> CancelReservationAsync(Guid reservationGuid);
    }
}
