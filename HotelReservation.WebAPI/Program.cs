
using FluentValidation;
using FluentValidation.AspNetCore;
using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.Contracts.Security;
using HotelReservation.Application.Contracts.Validation;
using HotelReservation.Application.UseCases.User;
using HotelReservation.Application.UseCases.User.Validation;
using HotelReservation.Domain.Repositories.DataManagement;
using HotelReservation.Infrastructure.Persistence.EFCore.Context;
using HotelReservation.Infrastructure.Persistence.Repositories.EntityFrameworkCore.RepositoryAndUnitOfWork;
using HotelReservation.Infrastructure.Security;
using HotelReservation.Infrastructure.Validation.FluentValidation;

namespace HotelReservation.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<HotelReservationAPIContext>();
            builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            builder.Services.AddScoped<IUserService, UserManager>();
            builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IGenericValidator, FluentValidator>();
            
            
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            
            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
