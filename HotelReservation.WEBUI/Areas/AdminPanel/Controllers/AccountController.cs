using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using System.Net;

namespace HotelReservation.WebUI.Areas.AdminPanel.Controllers
{
    public class AccountController : Controller
    {
        [Area("AdminPanel")]
        public class AccountController : Controller
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public AccountController(IHttpContextAccessor contextAccessor)
            {
                _httpContextAccessor = contextAccessor;
            }

            [HttpGet("/AdminAccount/Login")]
            public IActionResult Index()
            {
                _httpContextAccessor.HttpContext.Session.Clear();
                return View();
            }

            [HttpPost("/Account/AdminLogin")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AdminLogin(LoginDTO loginDTO)
            {
                var url = "http://localhost:5183/Login";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(loginDTO);
                request.AddBody(body, "application/json");
                RestResponse restResponse = await client.ExecuteAsync(request);

                var responseObject = JsonConvert.DeserializeObject<ApiResult<LoginDTO>>(restResponse.Content);

                if (restResponse.StatusCode == HttpStatusCode.NotFound && responseObject?.Data == null)
                {
                    ViewBag.LoginError = "Buraya Bir Data Geliyor";
                    ViewData["LoginError"] = "Kullanıcı Adı Veya Şifre Yanlış";
                    TempData["LoginError"] = "Buraya Başka Bir Data Geliyor";
                    return View("Index");
                }
                else if (restResponse.StatusCode != HttpStatusCode.OK)
                {
                    ViewData["LoginError"] = "Hata Oluştu";
                    return View("Index");
                }

                //_httpContextAccessor.HttpContext.Session.SetString("UserAdSoyad",responseObject.Data.AdSoyad);

                //_httpContextAccessor.HttpContext.Session.SetString("UserID", responseObject.Data.UserID.ToString());

                //_httpContextAccessor.HttpContext.Session.SetString("EPosta", responseObject.Data.EPosta.ToString());

                SessionManager.LoggedUser = responseObject.Data;

                return RedirectToAction("Index", "Home");
            }

        }
    }
}
