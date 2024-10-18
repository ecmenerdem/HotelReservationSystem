namespace HotelReservation.Application.DTO.Room
{
    public class RoomUpdateRequestDTO
    {
        public Guid RoomGuid { get; set; }
        public Guid HotelGuid { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }
}
