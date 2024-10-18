﻿namespace HotelReservation.Application.DTO.Reservation
{
    public class ReservationAddRequestDTO
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
