using HotelReservation.Domain.Entities.Base;

namespace HotelReservation.Domain.Entities;

public class Room : AuditableEntity
{
    public Room()
    {
        Reservations = new HashSet<Reservation>();
        RoomImages = new HashSet<RoomImage>(); // Resimlerle ilişkiyi initialize ediyoruz
    }

    public int HotelId { get; set; }
    public string RoomType { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; }

    public Hotel Hotel { get; set; }
    public ICollection<Reservation> Reservations { get; set; }

    // Navigation property for Room Images
    public IEnumerable<RoomImage> RoomImages { get; set; } // Oda resimleri
}