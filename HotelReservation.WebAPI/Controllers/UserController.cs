using System.Net;
using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.DTO.User;
using HotelReservation.Application.Result;
using HotelReservation.Domain.Exceptions;
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
    [ProducesResponseType(typeof(List<UserDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost("/User")]
    public async Task<IActionResult> AddUser([FromBody] UserAddRequestDTO userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _userService.AddUserAsync(userDto);

        return Created("", new { message = "User added successfully" });
    }

    // POST: api/User/login
    [HttpPost("/Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loginResponse = await _userService.LoginAsync(loginRequestDTO);

        if (loginResponse.StatusCode == HttpStatusCode.OK)
        {
            return Ok(loginResponse);
        }

        return NotFound(loginResponse);
    }
}