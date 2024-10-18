using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Exceptions
{
    public class InvalidReservationDatesException : Exception
    {
        public InvalidReservationDatesException()
            : base("Giriş tarihi, çıkış tarihinden önce olmalıdır.")
        {
        }
       
    }
}
