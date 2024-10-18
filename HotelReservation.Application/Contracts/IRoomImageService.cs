using HotelReservation.Application.DTO.RoomImage;

namespace HotelReservation.Application.Contracts
{
    public interface IRoomImageService
    {
        Task<RoomImageDTO> GetRoomImageByIdAsync(int roomImageId);
        Task<IEnumerable<RoomImageDTO>> GetImagesByRoomIdAsync(int roomId);
        Task AddRoomImageAsync(RoomImageAddRequestDTO roomImageDto);
        Task DeleteRoomImageAsync(int roomImageId);
    }
}
