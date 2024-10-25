using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException():base("Kullanıcı bulunamadı.")
        {

        }
        
        public UserNotFoundException(string username)
            : base($"'{username}' kullanıcı adına sahip kullanıcı bulunamadı.")
        {
        }

        public UserNotFoundException(int userId)
            : base($"ID'si {userId} olan kullanıcı bulunamadı.")
        {
        }


    }
}
