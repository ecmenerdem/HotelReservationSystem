using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.WebHelper.Result
{
    public class ApiResult<T>
    {
        public T? Data { get; set; }
        public string Mesaj { get; set; }

        public HataBilgisi HataBilgisi { get; set; }
    }
}
