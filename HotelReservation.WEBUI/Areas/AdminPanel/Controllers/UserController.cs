using System.Text.Json;
using HotelReservation.WebHelper.APIHelper.Request;
using HotelReservation.WebHelper.APIHelper.Result;
using HotelReservation.WebHelper.APIHelper.Service;
using HotelReservation.WebHelper.Const;
using HotelReservation.WebHelper.DTO.Account.Login;
using HotelReservation.WebHelper.DTO.User;
using HotelReservation.WebHelper.SessionHelper;
using HotelReservation.WebUI.Areas.AdminPanel.Filters;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace HotelReservation.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AuthorizeRoles("Admin", "Muhasebe")]

    public class UserController : Controller
    {
        private readonly IApiService _apiService;

        public UserController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet("/Admin/Kullanicilar")]
        public async Task<IActionResult> Index()
        {
            ApiRequest<GetUserListRequestDTO> request = new()
            {
                URL = "/Users",
                Method = HttpMethod.Get,
                Token = SessionManager.Token
            };

            var response = await _apiService.SendRequestAsync<GetUserListRequestDTO, List<UserDetailDTO>>(request);

            return View(response.Data);

        }

        [HttpGet("/Admin/User/{guid}")]
        public async Task<IActionResult> GetUserDetail(Guid guid)
        {
            var client = new RestClient();
            var request = new RestRequest($"{ApiEndpoint.ApiEndpointURL}/User/" + guid, Method.Get);

            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);


            var apiResponse = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<UserDetailDTO>>(apiResponse.Content);

            var user = responseObject.Data;

            return Json(new { success = true, user });
        }
    }
}
