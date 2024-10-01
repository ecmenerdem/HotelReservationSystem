﻿using HotelReservation.Domain.Entities.Base;

namespace HotelReservation.Domain.Entities;

public class Room : AuditableEntity
{
    public Room()
    {
        Reservations = new HashSet<Reservation>();
    }
    public int HotelId { get; set; }
    public string RoomType { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; }

    public Hotel Hotel { get; set; }
    public IEnumerable<Reservation> Reservations { get; set; }
}