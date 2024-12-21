namespace HotelReservation.Application.DTO.Reservation
{
    public class ReservationDTO
    {
        public Guid Guid { get; set; } // Unique Identifier for the Reservation
        public Guid UserGuid { get; set; }
        public Guid RoomGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // Confirmed, Pending, Cancelled
    }
}
