using HotelReservation.WebUI.Areas.AdminPanel.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AuthorizeRoles("Admin", "Muhasebe")]

    public class HomeController : Controller
    {
        [HttpGet("/Admin/Anasayfa")]

        public IActionResult Index()
        {

            return View();
        }
    }
}
