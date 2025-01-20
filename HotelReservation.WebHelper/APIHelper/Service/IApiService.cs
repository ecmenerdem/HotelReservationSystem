using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.WebHelper.APIHelper.Request;
using HotelReservation.WebHelper.APIHelper.Result;

namespace HotelReservation.WebHelper.APIHelper.Service
{
    public interface IApiService
    {
        Task<ApiResult<TResponse>> SendRequestAsync<TRequest, TResponse>(ApiRequest<TRequest> request);

    }
}
