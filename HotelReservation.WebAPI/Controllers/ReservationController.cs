using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.DTO.Reservation;
using HotelReservation.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.WebAPI.Controllers;

[ApiController]
[Route("HotelReservationApi/[action]")]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    // Get Available Rooms for a Hotel and Date Range
    [HttpGet("/AvailableRooms/{hotelGuid}")]
    public async Task<IActionResult> GetAvailableRooms(Guid hotelGuid, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (startDate >= endDate)
        {
            throw new InvalidReservationDatesException();
        }

        var result = await _reservationService.GetAvailableRoomsAsync(hotelGuid, startDate, endDate);
        return StatusCode((int)result.StatusCode, result);
    }

    // Get Reservation by GUID
    [HttpGet("/Reservation/{guid}")]
    public async Task<IActionResult> GetReservationByGUID(Guid guid)
    {
        var result = await _reservationService.GetReservationByGuidAsync(guid);
        return StatusCode((int)result.StatusCode, result);
    }

    // Get Reservations by User
    [HttpGet("/ReservationsByUser/{userGuid}")]
    public async Task<IActionResult> GetReservationsByUser(Guid userGuid)
    {
        var result = await _reservationService.GetReservationsByUserAsync(userGuid);
        return StatusCode((int)result.StatusCode, result);
    }

    // Add Reservation
    [HttpPost("/Reservation")]
    public async Task<IActionResult> AddReservation([FromBody] ReservationAddRequestDTO reservationDto)
    {
        if (reservationDto.StartDate >= reservationDto.EndDate)
        {
            return BadRequest(new { Message = "Başlangıç tarihi bitiş tarihinden önce olmalıdır." });
        }

        var result = await _reservationService.AddReservationAsync(reservationDto);
        return StatusCode((int)result.StatusCode, result);
    }

    // Cancel Reservation
    [HttpDelete("/Reservation/{guid}")]
    public async Task<IActionResult> CancelReservation(Guid guid)
    {
        var result = await _reservationService.CancelReservationAsync(guid);
        return StatusCode((int)result.StatusCode, result);
    }
}