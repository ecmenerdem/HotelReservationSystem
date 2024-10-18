using HotelReservation.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities
{
    public class RoomImage : AuditableEntity
    {
        public int RoomId { get; set; }
        public string ImageUrl { get; set; }

        // Navigation Property
        public Room Room { get; set; }
    }
}
