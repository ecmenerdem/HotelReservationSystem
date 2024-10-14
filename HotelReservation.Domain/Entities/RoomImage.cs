using HotelReservation.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities
{
    public class RoomImage: BaseEntity
    {
        public int Id { get; set; }
        public int RoomId { get; set; } // Odayla ilişki
        public string ImagePath { get; set; } // Resim dosya yolu
        public string Description { get; set; } // Resim açıklaması (opsiyonel)

        // Navigation Property
        public Room Room { get; set; } // Oda ile ilişkili
    }
}
