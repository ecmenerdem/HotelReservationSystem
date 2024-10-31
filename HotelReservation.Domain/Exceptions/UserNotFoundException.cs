using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Domain.Const;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(HttpStatusCode statusCode= HttpStatusCode.NotFound):base("Kullanıcı bulunamadı.")
        {
            AppContextManager.ResponseStatusCode = statusCode;
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
