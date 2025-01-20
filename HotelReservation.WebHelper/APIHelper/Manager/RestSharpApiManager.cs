using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HotelReservation.WebHelper.APIHelper.Request;
using HotelReservation.WebHelper.APIHelper.Result;
using RestSharp;
using HotelReservation.WebHelper.Const;
using HotelReservation.WebHelper.APIHelper.Service;

namespace HotelReservation.WebHelper.APIHelper.Manager
{
    public class RestSharpApiManager: IApiService
    {
        public async Task<ApiResult<TResponse>> SendRequestAsync<TRequest, TResponse>(ApiRequest<TRequest> request)
        {

            var method = Enum.Parse<Method>(request.Method.ToString(), true);

            var url = ApiEndpoint.ApiEndpointURL + request.URL;
            var client = new RestClient(url);
            var restRequest = new RestRequest(url, method);

            if (request.Token is not null)
            {
                restRequest.AddHeader("Authorization", "Bearer " + request.Token);
            }

           
            if (request.Body is not null)
            {
                restRequest.AddBody(request.Body, "application/json");
            }
            RestResponse restResponse = await client.ExecuteAsync(restRequest);

            var responseObject = JsonSerializer.Deserialize<ApiResult<TResponse>>(restResponse.Content);
            responseObject.StatusCode= restResponse.StatusCode;
            return responseObject;
        }
    }
}
