using HotelReservation.Application.DTO.Room;

namespace HotelReservation.Application.Contracts
{
    public interface IRoomService
    {
        Task<RoomDTO> GetRoomByIdAsync(int roomId);
        Task<IEnumerable<RoomDTO>> GetAllRoomsAsync();
        Task<IEnumerable<RoomDTO>> GetAvailableRoomsAsync(int hotelId);
        Task AddRoomAsync(RoomAddRequestDTO roomDto);
        Task UpdateRoomAsync(RoomUpdateRequestDTO roomDto);
        Task DeleteRoomAsync(int roomId);
    }
}
