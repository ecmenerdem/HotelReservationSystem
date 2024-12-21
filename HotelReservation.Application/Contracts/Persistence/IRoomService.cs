using HotelReservation.Application.DTO.Room;
using HotelReservation.Application.Result;

namespace HotelReservation.Application.Contracts.Persistence
{
    public interface IRoomService
    {
        Task<ApiResult<RoomDTO>> GetRoomByIdAsync(int roomId);
        Task<ApiResult<RoomDTO>> GetRoomByGUIDAsync(Guid roomGUID);
        Task<ApiResult<IEnumerable<RoomDTO>>> GetAllRoomsAsync();
        Task<ApiResult<IEnumerable<RoomDTO>>> GetAvailableRoomsAsync(Guid hotelGUID);
        Task<ApiResult<RoomDTO>>AddRoomAsync(RoomAddRequestDTO roomDto);
        Task<ApiResult<bool>> UpdateRoomAsync(RoomUpdateRequestDTO roomDto);
        Task<ApiResult<bool>> DeleteRoomAsync(Guid roomGUID);
    }
}
