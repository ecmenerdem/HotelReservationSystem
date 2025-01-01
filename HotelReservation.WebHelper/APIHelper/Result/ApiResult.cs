using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelReservation.WebHelper.APIHelper.Result
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ErrorResult Error { get; set; }


        [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

    }
}
