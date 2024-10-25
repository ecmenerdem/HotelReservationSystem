using HotelReservation.Application.DTO.Reservation;

namespace HotelReservation.Application.Contracts.Persistence
{
    public interface IReservationService
    {
        Task<ReservationDTO> GetReservationByIdAsync(int reservationId);
        Task<IEnumerable<ReservationDTO>> GetAllReservationsAsync();
        Task<IEnumerable<ReservationDTO>> GetReservationsByUserAsync(int userId);
        Task AddReservationAsync(ReservationAddRequestDTO reservationDto);
        Task CancelReservationAsync(int reservationId);
    }
}
