using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Entities.Base
{
    public class BaseEntity
    {
      
        public int ID { get; set; }
        public Guid GUID { get; set; }=Guid.NewGuid();

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
