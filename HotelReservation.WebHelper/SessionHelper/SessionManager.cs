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
    }
}
