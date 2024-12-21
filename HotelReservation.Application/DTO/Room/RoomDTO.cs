namespace HotelReservation.Application.DTO.Room
{
    public class RoomDTO
    {
        public Guid GUID { get; set; }
        //public int HotelId { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
