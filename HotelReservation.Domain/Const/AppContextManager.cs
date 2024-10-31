using System.Net;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Domain.Const;

public static class AppContextManager
{
    /*Ecmen*/
    public static HttpStatusCode ResponseStatusCode
    {
        get
        {
            return (HttpStatusCode)AppContext.GetData("ResponseStatusCode");
        }

        set
        {
            if (value == null)
            {
                AppContext.SetData("ResponseStatusCode",HttpStatusCode.InternalServerError);
            }
            else
            {
                AppContext.SetData("ResponseStatusCode",value);
            }
        }
    }
}