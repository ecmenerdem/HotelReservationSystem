using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using HotelReservation.WebHelper.APIHelper.Contract;
using HotelReservation.WebHelper.APIHelper.Request;
using HotelReservation.WebHelper.Const;
using HotelReservation.WebHelper.DTO.Account.Login;
using HotelReservation.WebHelper.SessionHelper;
using HotelReservation.WebUI.Areas.AdminPanel.Filters;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using RestSharp;

namespace HotelReservation.WebUI.Areas.AdminPanel.Controllers
{

    [Area("AdminPanel")]
    //[Route("[action]")]

    public class AccountController : Controller
    {
        private readonly IApiService _apiService;

        public AccountController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet("/Admin/Login")]
        public IActionResult LoginPage(string? erroMessage)
        {
            ViewData["LoginError"] = erroMessage;
            ViewData["LoginError"] = HttpContext.Items["LoginError"];
            return View();
        }

        [HttpPost("/Account/AdminLogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginRequestDTO loginRequest)
        {
            ApiRequest<LoginRequestDTO> request = new()
            {
                URL = "/Login",
                Method = HttpMethod.Post,
                Body = loginRequest

            };

            var response = await _apiService.SendRequestAsync<LoginRequestDTO, LoginResponseDTO>(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ViewData["LoginError"] = "Kullanıcı Adı Veya Şifre Yanlış";
                return View("LoginPage");
            }
            else
            {

                SessionManager.loginResponseDTO = response.Data;
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
