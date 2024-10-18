using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.Exceptions
{
    public class InvalidUserCredentialsException : Exception
    {
        public InvalidUserCredentialsException()
            : base("Geçersiz kullanıcı adı veya şifre.")
        {
        }

        public InvalidUserCredentialsException(string username)
            : base($"'{username}' kullanıcı adına sahip kullanıcı için geçersiz kimlik bilgileri sağlandı.")
        {
        }
    }
}
