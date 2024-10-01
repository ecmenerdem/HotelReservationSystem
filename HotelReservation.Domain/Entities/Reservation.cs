using HotelReservation.Domain.Entities.Base;

namespace HotelReservation.Domain.Entities;

public class Reservation : AuditableEntity
{
    public int HotelId { get; set; }
    public int RoomId { get; set; }
    public int UserId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }

    public User User { get; set; }
    public Room Room { get; set; }
    public Hotel Hotel { get; set; }
}