using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.WebHelper.DTO.Account.Login;

namespace HotelReservation.WebHelper.SessionHelper
{
    public class SessionManager
    {
        public static LoginResponseDTO? loginResponseDTO
        {
            get => AppHttpContext.Current.Session.GetObject<LoginResponseDTO>("LoginResponseDTO");
            set => AppHttpContext.Current.Session.SetObject("LoginResponseDTO", value);
        }

        public static string ExceptionMessage
        {
            get => AppHttpContext.Current.Session.GetObject<string>("exceptionMessage");
            set => AppHttpContext.Current.Session.SetObject("exceptionMessage", value);

        }

        public static string Token
        {
            get => AppHttpContext.Current.Session.GetObject<string>("Token");
            set => AppHttpContext.Current.Session.SetObject("Token", value);

        }
    }
}
