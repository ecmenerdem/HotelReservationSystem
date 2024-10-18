using HotelReservation.Domain.Entities.Base;

namespace HotelReservation.Domain.Entities;

public class Hotel : AuditableEntity
{
    public Hotel()
    {
        Rooms = new HashSet<Room>();
    }

    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    // Navigation Properties
    public IEnumerable<Room> Rooms { get; private set; }

   
}