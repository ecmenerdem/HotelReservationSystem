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
        public int StarRating { get; set; }
    }
}
