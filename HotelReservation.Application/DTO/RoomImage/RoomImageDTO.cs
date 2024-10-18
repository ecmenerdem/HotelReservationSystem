namespace HotelReservation.Application.DTO.RoomImage
{
    public class RoomImageDTO
    {
        public Guid GUID { get; set; }
        public int RoomId { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
    } 
}
