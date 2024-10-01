using HotelReservation.Domain.Entities.Base;

namespace HotelReservation.Domain.Entities;

public class Hotel : AuditableEntity
{
    public Hotel()
    {
        Rooms = new HashSet<Room>();
        Reservations = new HashSet<Reservation>();
    }
    public string Name { get; set; }
    public string Address { get; set; }
    public int StarRating { get; set; }

    public IEnumerable<Room> Rooms { get; set; }
    public IEnumerable<Reservation> Reservations { get; set; }
}