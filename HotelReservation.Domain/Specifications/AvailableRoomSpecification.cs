using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Specifications
{
    public class AvailableRoomSpecification
    {
        public bool IsAvalibleForPurchase(Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            return room.IsAvailable && checkInDate < checkOutDate;
        }
    }
}
