﻿using HotelReservation.Domain.Entities.Base;

namespace HotelReservation.Domain.Entities;

public class Room : AuditableEntity
{

    public Room()
    {
        RoomImages = new HashSet<RoomImage>();
        Reservations = new HashSet<Reservation>();
    }

    public int HotelId { get; set; }
    public string RoomNumber { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public string Description { get; set; }

    // Navigation Properties
    public Hotel Hotel { get; set; }
    public IEnumerable<RoomImage> RoomImages { get; private set; }
    public IEnumerable<Reservation> Reservations { get; private set; }

    
}
