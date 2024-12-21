using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.DTO.Room;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.WebAPI.Controllers;

[ApiController]
[Route("HotelReservationApi/[action]")]
public class RoomController : Controller
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("/Room/{guid}")]
    public async Task<IActionResult> GetRoomByGUID(Guid guid)
    {
        var result = await _roomService.GetRoomByGUIDAsync(guid);
        return StatusCode((int)result.StatusCode, result);
    }

    // Get All Rooms
    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var result = await _roomService.GetAllRoomsAsync();
        return StatusCode((int)result.StatusCode, result);
    }

    // Get Available Rooms for a Hotel
    [HttpGet("/AvaliableRooms/{hotelGUID}")]
    public async Task<IActionResult> GetAvailableRooms(Guid hotelGUID)
    {
        var result = await _roomService.GetAvailableRoomsAsync(hotelGUID);
        return StatusCode((int)result.StatusCode, result);
    }

    // Add Room
    [HttpPost("/Room")]
    public async Task<IActionResult> AddRoom([FromBody] RoomAddRequestDTO roomDto)
    {
       var result = await _roomService.AddRoomAsync(roomDto);
       return StatusCode((int)result.StatusCode, result);

        
    }

    // Update Room
    [HttpPut("/Room")]
    public async Task<IActionResult> UpdateRoom([FromBody] RoomUpdateRequestDTO roomDto)
    {
       var result = await _roomService.UpdateRoomAsync(roomDto);
       return StatusCode((int)result.StatusCode, result);
    }

    // Soft Delete Room
    [HttpDelete("/Room/{guid}")]
    public async Task<IActionResult> DeleteRoom(Guid guid)
    {
        var result = await _roomService.DeleteRoomAsync(guid);
        return StatusCode((int)result.StatusCode, result);

    }
}
