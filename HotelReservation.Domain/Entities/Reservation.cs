using HotelReservation.Domain.Entities.Base;

namespace HotelReservation.Domain.Entities;

public class Reservation : AuditableEntity
{
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfGuests { get; set; }
    public decimal TotalPrice { get; set; }

    // Navigation Properties
    public User User { get; set; }
    public Room Room { get; set; }
}
