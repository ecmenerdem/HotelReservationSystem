using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Exceptions
{
    public class HotelOverbookingException : Exception
    {
        public HotelOverbookingException(int hotelId)
            : base($"ID'si {hotelId} olan otel, maksimum kapasiteye ulaşmış ve seçilen tarihler için daha fazla rezervasyon kabul edemez.")
        {
        }

        public HotelOverbookingException(int hotelId, string checkInDate, string checkOutDate)
            : base($"ID'si {hotelId} olan otel, {checkInDate} ile {checkOutDate} tarihleri arasında aşırı dolu.")
        {
        }
    }
}
