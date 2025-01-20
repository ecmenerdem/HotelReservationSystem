using HotelReservation.WebHelper;
using HotelReservation.WebHelper.APIHelper.Manager;
using HotelReservation.WebHelper.APIHelper.Service;
using HotelReservation.WebUI.Middleware;

namespace HotelReservation.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();

            // AppHttpContext'e DI ile eriþimi yapýlandýr
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<AppHttpContext>();
            builder.Services.AddScoped<IApiService, RestSharpApiManager>();

            builder.Services.AddAntiforgery(opt => opt.HeaderName = "XSRF-Token");

            var app = builder.Build();
            app.UseSession();
            var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
            AppHttpContext.Configure(httpContextAccessor);


            app.UseGlobalExceptionHandlerMiddleware();
            app.UseSessionNullCheckMiddleware();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
