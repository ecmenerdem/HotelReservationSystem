namespace HotelReservation.Application.DTO.Hotel
{
    public class HotelUpdateRequestDTO
    {
        public Guid GUID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int StarRating { get; set; }
    }
}
