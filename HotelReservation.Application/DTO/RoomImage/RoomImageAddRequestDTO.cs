namespace HotelReservation.Application.DTO.RoomImage
{
    public class RoomImageAddRequestDTO
    {
        public Guid RoomGuid { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
    }
}
