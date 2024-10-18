namespace HotelReservation.Application.DTO.Room
{
    public class RoomAddRequestDTO
    {
        public Guid HotelGuid { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }
}
