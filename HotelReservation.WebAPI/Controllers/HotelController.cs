using AutoMapper;
using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.DTO.Hotel;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.WebAPI.Controllers;

[ApiController]
[Route("HotelReservationApi/[action]")]
public class HotelController : Controller
{
    private readonly IHotelService _hotelService;
    private readonly IMapper _mapper;

    public HotelController(IHotelService hotelService, IMapper mapper)
    {
        _hotelService = hotelService;
        _mapper = mapper;
    }

    // Get Hotel by ID
    [HttpGet("/Hotel/{id}")]
    public async Task<IActionResult> GetHotelById(int id)
    {
        var result = await _hotelService.GetHotelByIdAsync(id);
        return StatusCode((int)result.StatusCode, result.Error);
    }

    // Get All Hotels
    [HttpGet("/Hotels")]
    public async Task<IActionResult> GetAllHotels()
    {
        var result = await _hotelService.GetAllHotelsAsync();
        return StatusCode((int)result.StatusCode, result.Error);
    }

    // Add a New Hotel
    [HttpPost("/Hotel")]
    public async Task<IActionResult> AddHotel([FromBody] HotelAddRequestDTO hotelAddRequestDto)
    {
        var result = await _hotelService.AddHotelAsync(hotelAddRequestDto);
        return StatusCode((int)result.StatusCode, result.Error);
    }

    // Update Hotel
    [HttpPut("/Hotel/{id}")]
    public async Task<IActionResult> UpdateHotel([FromBody] HotelUpdateRequestDTO hotelUpdateRequestDto)
    {
        var result = await _hotelService.UpdateHotelAsync(hotelUpdateRequestDto);
        return StatusCode((int)result.StatusCode, result.Error);
    }

    // Delete Hotel
    [HttpDelete("/Hotel/{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var result = await _hotelService.DeleteHotelAsync(id);
        return StatusCode((int)result.StatusCode, result.Error);
    }
}