using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.WebHelper
{
    public class AppHttpContext
    {
        /*

         IHttpContextAccessor erişebilmek için projeye eklenmesi gereken paketler:
         <ItemGroup>
           <FrameworkReference Include="Microsoft.AspNetCore.App" />
           </ItemGroup>
        
         */

        private static IHttpContextAccessor _httpContextAccessor;

        public static HttpContext Current => _httpContextAccessor?.HttpContext;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
