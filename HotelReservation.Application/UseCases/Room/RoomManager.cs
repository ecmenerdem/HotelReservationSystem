using System.Net;
using AutoMapper;
using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.Contracts.Validation;
using HotelReservation.Application.DTO.Room;
using HotelReservation.Application.Result;
using HotelReservation.Application.UseCases.Room.Validation;
using HotelReservation.Domain.Repositories.DataManagement;

namespace HotelReservation.Application.UseCases.Room;

public class RoomManager:IRoomService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IGenericValidator _validator;

    public RoomManager(IUnitOfWork uow, IMapper mapper, IGenericValidator validator)
    {
        _uow = uow;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<ApiResult<RoomDTO>> GetRoomByIdAsync(int roomId)
    {
        var room = await _uow.RoomRepository.GetAsync(r => r.ID == roomId);

        if (room == null)
        {
            var error = new ErrorResult(new List<string> { "Oda bulunamadı" });
            return ApiResult<RoomDTO>.FailureResult(error, HttpStatusCode.NotFound);
        }

        var roomDto = _mapper.Map<RoomDTO>(room);
        return ApiResult<RoomDTO>.SuccessResult(roomDto);
    }

    public async Task<ApiResult<RoomDTO>> GetRoomByGUIDAsync(Guid roomGUID)
    {
        var room = await _uow.RoomRepository.GetAsync(r => r.GUID == roomGUID);

        if (room == null)
        {
            var error = new ErrorResult(new List<string> { "Oda bulunamadı" });
            return ApiResult<RoomDTO>.FailureResult(error, HttpStatusCode.NotFound);
        }

        var roomDto = _mapper.Map<RoomDTO>(room);
        return ApiResult<RoomDTO>.SuccessResult(roomDto);
    }

    public async Task<ApiResult<IEnumerable<RoomDTO>>> GetAllRoomsAsync()
    {
        var rooms = await _uow.RoomRepository.GetAllAsync();

        if (!rooms.Any())
        {
            var error = new ErrorResult(new List<string> { "Hiç oda bulunamadı" });
            return ApiResult<IEnumerable<RoomDTO>>.FailureResult(error, HttpStatusCode.NotFound);
        }

        var roomDtos = _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        return ApiResult<IEnumerable<RoomDTO>>.SuccessResult(roomDtos);
    }

    public async Task<ApiResult<IEnumerable<RoomDTO>>> GetAvailableRoomsAsync(Guid hotelGUID)
    {
        var rooms = await _uow.RoomRepository.GetAllAsync(r => r.Hotel.GUID == hotelGUID && r.IsAvailable);

        if (!rooms.Any())
        {
            var error = new ErrorResult(new List<string> { "Uygun oda bulunamadı" });
            return ApiResult<IEnumerable<RoomDTO>>.FailureResult(error, HttpStatusCode.NotFound);
        }

        var roomDtos = _mapper.Map<IEnumerable<RoomDTO>>(rooms);
        return ApiResult<IEnumerable<RoomDTO>>.SuccessResult(roomDtos);
    }

    public async Task<ApiResult<RoomDTO>> AddRoomAsync(RoomAddRequestDTO roomDto)
    {
        await _validator.ValidateAsync(roomDto, typeof(AddRoomValidator));
        var room = _mapper.Map<Domain.Entities.Room>(roomDto);
        await _uow.RoomRepository.AddAsync(room);
        await _uow.SaveAsync();

        var roomDtoResult = _mapper.Map<RoomDTO>(room);
        return ApiResult<RoomDTO>.SuccessResult(roomDtoResult);
    }

    public async Task<ApiResult<bool>> UpdateRoomAsync(RoomUpdateRequestDTO roomDto)
    {
        await _validator.ValidateAsync(roomDto, typeof(UpdateRoomValidator));

        var room = await _uow.RoomRepository.GetAsync(r => r.GUID == roomDto.RoomGuid);
        if (room == null)
        {
            var error = new ErrorResult(new List<string> { "Güncellenecek oda bulunamadı" });
            return ApiResult<bool>.FailureResult(error, HttpStatusCode.NotFound);
        }

        _mapper.Map(roomDto, room);
        _uow.RoomRepository.Update(room);
        await _uow.SaveAsync();

        return ApiResult<bool>.SuccessResult(true);
    }

    public async Task<ApiResult<bool>> DeleteRoomAsync(Guid roomGUID)
    {
        // Odayı veritabanından getir
        var room = await _uow.RoomRepository.GetAsync(r => r.GUID == roomGUID);

        // Oda bulunamazsa hata döndür
        if (room == null)
        {
            var error = new ErrorResult(new List<string> { "Silinecek oda bulunamadı" });
            return ApiResult<bool>.FailureResult(error, HttpStatusCode.NotFound);
        }

        // Soft delete işlemi:
        room.IsActive = false;
        room.IsDeleted = true;
        _uow.RoomRepository.Update(room);
        await _uow.SaveAsync();

        // Başarı yanıtı döndür
        return ApiResult<bool>.SuccessResult(true);
    }

    
}