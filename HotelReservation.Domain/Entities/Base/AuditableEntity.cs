using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities.Base
{
    public class AuditableEntity:BaseEntity
    {
        // Oluşturma bilgileri
        public int? AddedUser { get; set; }
        public DateTime? AddedTime { get; set; }
        public string? AddedIP { get; set; }

        // Güncelleme bilgileri
        public int? UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdatedIP { get; set; }

       
    }
}
