using HotelReservation.WebHelper.DTO.Account.Login;
using HotelReservation.WebHelper.SessionHelper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.WebUI.Areas.AdminPanel.Filters
{
    public class AuthorizeRolesAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string[] _roles;

        public AuthorizeRolesAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = SessionManager.loginResponseDTO;
            if (user == null || !_roles.Contains(user.GroupName))
            {
                context.HttpContext.Items["LoginError"] = "Bu İşlemi Yapmaya Yetkiniz Yoktur";
                var erroMessage = "Bu İşlemi Yapmaya Yetkiniz Yoktur";
                context.HttpContext.Response.Redirect("/Admin/Login"); 
                
            }
        }
    }

}
