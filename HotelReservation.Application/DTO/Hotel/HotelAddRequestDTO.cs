using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.DTO.Hotel
{
    public class HotelAddRequestDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
       
        public string City { get; set; }
        
        public string Description { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
    }
}
