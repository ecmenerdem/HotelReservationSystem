using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using HotelReservation.WebHelper.Const;
using HotelReservation.WebHelper.DTO.Account.Login;
using HotelReservation.WebHelper.Result;
using HotelReservation.WebHelper.SessionHelper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity.Data;
using RestSharp;

namespace HotelReservation.WebUI.Areas.AdminPanel.Controllers
{

    [Area("AdminPanel")]
    [Route("[action]")]

    public class AccountController : Controller
    {
       
        [HttpGet("/AdminAccount/Login")]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost("/Account/AdminLogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginRequestDTO loginRequest)
        {
            var url = ApiEndpoint.ApiEndpointURL + "/Login";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonSerializer.Serialize(loginRequest);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);

            var responseObject = JsonSerializer.Deserialize<ApiResult<LoginResponseDTO>>(response.Content);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ViewData["LoginError"] = "Kullanıcı Adı Veya Şifre Yanlış";
                return View("LoginPage");
            }
            else
            {

                SessionManager.loginResponseDTO = responseObject.Data;
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            SessionManager.loginResponseDTO = null;
            return RedirectToAction("LoginPage", "Account");
        }

    }

}
