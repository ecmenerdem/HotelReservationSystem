using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Exceptions
{
    public class RoomNotAvailableException : Exception
    {
        public RoomNotAvailableException(int roomId)
            : base($"ID'si {roomId} olan oda, seçilen tarihlerde müsait değil.")
        {
        }

        public RoomNotAvailableException(int roomId, string checkInDate, string checkOutDate)
            : base($"ID'si {roomId} olan oda, {checkInDate} ile {checkOutDate} tarihleri arasında müsait değil.")
        {
        }
    }
}
