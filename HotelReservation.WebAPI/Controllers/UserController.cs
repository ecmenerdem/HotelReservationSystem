using HotelReservation.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.WebAPI.Controllers;

[Route("HotelReservationApi/[action]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("/Users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
}