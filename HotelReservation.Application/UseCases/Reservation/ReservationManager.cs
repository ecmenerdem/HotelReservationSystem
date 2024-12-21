using System.Net;
using AutoMapper;
using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.DTO.Reservation;
using HotelReservation.Application.DTO.Room;
using HotelReservation.Application.Result;
using HotelReservation.Domain.Exceptions;
using HotelReservation.Domain.Repositories.DataManagement;

namespace HotelReservation.Application.UseCases.Reservation;

public class ReservationManager : IReservationService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ReservationManager(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResult<IEnumerable<RoomDTO>>> GetAvailableRoomsAsync(Guid hotelGuid, DateTime startDate, 
        DateTime endDate)
    {
        // Otelin tüm odalarını al
        var rooms = await _uow.RoomRepository.GetAllAsync(r => r.GUID == hotelGuid);

        // Rezervasyon yapılmış odaları al
        var reservedRoomGuidsQuery = await _uow.ReservationRepository
            .GetAllAsync(r => r.GUID == hotelGuid &&
                              (startDate < r.CheckOutDate && endDate > r.CheckInDate));
            
       var reservedRoomGuidList= reservedRoomGuidsQuery.Select(r => r.GUID).ToList();

        // Uygun odaları filtrele
        var availableRooms = rooms.Where(r => !reservedRoomGuidList.Contains(r.GUID)).ToList();

        // DTO'ya dönüştür
        var roomDtos = _mapper.Map<IEnumerable<RoomDTO>>(availableRooms);
        return ApiResult<IEnumerable<RoomDTO>>.SuccessResult(roomDtos);
    }

    public async Task<ApiResult<ReservationDTO>> GetReservationByGuidAsync(Guid reservationGuid)
    {
        var reservation = await _uow.ReservationRepository.GetAsync(r => r.GUID == reservationGuid);

        if (reservation == null)
        {
            var error = new ErrorResult(new List<string> { "Rezervasyon bulunamadı" });
            return ApiResult<ReservationDTO>.FailureResult(error, HttpStatusCode.NotFound);
        }

        var reservationDto = _mapper.Map<ReservationDTO>(reservation);
        return ApiResult<ReservationDTO>.SuccessResult(reservationDto);
    }

    public async Task<ApiResult<IEnumerable<ReservationDTO>>> GetAllReservationsAsync()
    {
        var reservations = await _uow.ReservationRepository.GetAllAsync();

        if (!reservations.Any())
        {
            var error = new ErrorResult(new List<string> { "Hiç rezervasyon bulunamadı" });
            return ApiResult<IEnumerable<ReservationDTO>>.FailureResult(error, HttpStatusCode.NotFound);
        }

        var reservationDtos = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);
        return ApiResult<IEnumerable<ReservationDTO>>.SuccessResult(reservationDtos);
    }

    public async Task<ApiResult<IEnumerable<ReservationDTO>>> GetReservationsByUserAsync(Guid userGuid)
    {
        var reservations = await _uow.ReservationRepository.GetAllAsync(r => r.GUID == userGuid);

        if (!reservations.Any())
        {
            var error = new ErrorResult(new List<string> { "Bu kullanıcı için rezervasyon bulunamadı" });
            return ApiResult<IEnumerable<ReservationDTO>>.FailureResult(error, HttpStatusCode.NotFound);
        }

        var reservationDtos = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);
        return ApiResult<IEnumerable<ReservationDTO>>.SuccessResult(reservationDtos);
    }

    public async Task<ApiResult<IEnumerable<ReservationDTO>>> GetReservationsByRoomAsync(Guid roomGuid)
    {
        var reservations = await _uow.ReservationRepository.GetAllAsync(r => r.GUID == roomGuid);

        if (!reservations.Any())
        {
            var error = new ErrorResult(new List<string> { "Bu oda için rezervasyon bulunamadı" });
            return ApiResult<IEnumerable<ReservationDTO>>.FailureResult(error, HttpStatusCode.NotFound);
        }

        var reservationDtos = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);
        return ApiResult<IEnumerable<ReservationDTO>>.SuccessResult(reservationDtos);
    }

    public async Task<ApiResult<ReservationDTO>> AddReservationAsync(ReservationAddRequestDTO reservationDto)
    {
        var room = await _uow.RoomRepository.GetAsync(r => r.GUID == reservationDto.RoomGuid);
        // Oda uygunluk kontrolü
        var isRoomAvailable = await _uow.ReservationRepository.GetAllAsync(r =>
            r.GUID == reservationDto.RoomGuid &&
            (reservationDto.StartDate < r.CheckOutDate && reservationDto.EndDate > r.CheckInDate));

        if (isRoomAvailable.Any())
        {
            throw new RoomNotAvailableException(room.ID,reservationDto.StartDate.ToShortDateString(), reservationDto.EndDate
                .ToShortDateString());
        }

        var reservation = _mapper.Map<Domain.Entities.Reservation>(reservationDto);

        await _uow.ReservationRepository.AddAsync(reservation);
        await _uow.SaveAsync();

        var reservationDtoResult = _mapper.Map<ReservationDTO>(reservation);
        return ApiResult<ReservationDTO>.SuccessResult(reservationDtoResult);
    }

    public async Task<ApiResult<bool>> CancelReservationAsync(Guid reservationGuid)
    {
        var reservation = await _uow.ReservationRepository.GetAsync(r => r.GUID == reservationGuid);

        if (reservation == null)
        {
            var error = new ErrorResult(new List<string> { "İptal edilecek rezervasyon bulunamadı" });
            return ApiResult<bool>.FailureResult(error, HttpStatusCode.NotFound);
        }

        _uow.ReservationRepository.Remove(reservation);
        await _uow.SaveAsync();

        return ApiResult<bool>.SuccessResult(true);
    }
}