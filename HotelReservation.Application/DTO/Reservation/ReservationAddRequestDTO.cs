namespace HotelReservation.Application.DTO.Reservation
{
    public class ReservationAddRequestDTO
    {
        public Guid UserGuid { get; set; }
        public Guid RoomGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
