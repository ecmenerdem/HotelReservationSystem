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
    private readonly IUnitOfWork _uow; // Birim iş birimi, veri erişim işlemleri için kullanılır.
    private readonly IMapper _mapper; // DTO'lar ile domain nesneleri arasında dönüşüm yapmak için kullanılır.

    public ReservationManager(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    /// <summary>
    /// Belirtilen tarihler arasında uygun odaları alır.
    /// </summary>
    /// <param name="hotelGuid">Otelin GUID'i.</param>
    /// <param name="startDate">Rezervasyon başlangıç tarihi.</param>
    /// <param name="endDate">Rezervasyon bitiş tarihi.</param>
    /// <returns>Uygun odaların DTO'ları.</returns>
    public async Task<ApiResult<IEnumerable<RoomDTO>>> GetAvailableRoomsAsync(Guid hotelGuid, DateTime startDate, 
        DateTime endDate)
    {
        // Otelin tüm odalarını al
        var rooms = await _uow.RoomRepository.GetAllAsync(r => r.GUID == hotelGuid);

        // Rezervasyon yapılmış odaları al
        var reservedRoomGuidsQuery = await _uow.ReservationRepository
            .GetAllAsync(r => r.GUID == hotelGuid &&
                              (startDate < r.CheckOutDate && endDate > r.CheckInDate));
            
        // Rezervasyon yapılmış odaların GUID'lerini listele
        var reservedRoomGuidList = reservedRoomGuidsQuery.Select(r => r.GUID).ToList();

        // Uygun odaları filtrele (rezervasyon yapılmamış olanlar)
        var availableRooms = rooms.Where(r => !reservedRoomGuidList.Contains(r.GUID)).ToList();

        // Uygun odaları DTO'ya dönüştür
        var roomDtos = _mapper.Map<IEnumerable<RoomDTO>>(availableRooms);
        return ApiResult<IEnumerable<RoomDTO>>.SuccessResult(roomDtos);
    }

    /// <summary>
    /// Belirtilen rezervasyon GUID'ine göre rezervasyonu alır.
    /// </summary>
    /// <param name="reservationGuid">Rezervasyonun GUID'i.</param>
    /// <returns>Rezervasyonun DTO'su.</returns>
    public async Task<ApiResult<ReservationDTO>> GetReservationByGuidAsync(Guid reservationGuid)
    {
        // Rezervasyonu GUID ile bul
        var reservation = await _uow.ReservationRepository.GetAsync(r => r.GUID == reservationGuid);

        // Rezervasyon bulunamazsa hata döndür
        if (reservation == null)
        {
            var error = new ErrorResult(new List<string> { "Rezervasyon bulunamadı" });
            return ApiResult<ReservationDTO>.FailureResult(error, HttpStatusCode.NotFound);
        }

        // Rezervasyonu DTO'ya dönüştür
        var reservationDto = _mapper.Map<ReservationDTO>(reservation);
        return ApiResult<ReservationDTO>.SuccessResult(reservationDto);
    }

    /// <summary>
    /// Tüm rezervasyonları alır.
    /// </summary>
    /// <returns>Tüm rezervasyonların DTO'ları.</returns>
    public async Task<ApiResult<IEnumerable<ReservationDTO>>> GetAllReservationsAsync()
    {
        // Tüm rezervasyonları al
        var reservations = await _uow.ReservationRepository.GetAllAsync();

        // Eğer rezervasyon yoksa hata döndür
        if (!reservations.Any())
        {
            var error = new ErrorResult(new List<string> { "Hiç rezervasyon bulunamadı" });
            return ApiResult<IEnumerable<ReservationDTO>>.FailureResult(error, HttpStatusCode.NotFound);
        }

        // Rezervasyonları DTO'ya dönüştür
        var reservationDtos = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);
        return ApiResult<IEnumerable<ReservationDTO>>.SuccessResult(reservationDtos);
    }

    /// <summary>
    /// Belirtilen kullanıcı GUID'ine göre rezervasyonları alır.
    /// </summary>
    /// <param name="userGuid">Kullanıcının GUID'i.</param>
    /// <returns>Kullanıcıya ait rezervasyonların DTO'ları.</returns>
    public async Task<ApiResult<IEnumerable<ReservationDTO>>> GetReservationsByUserAsync(Guid userGuid)
    {
        // Kullanıcıya ait rezervasyonları al
        var reservations = await _uow.ReservationRepository.GetAllAsync(r => r.GUID == userGuid);

        // Eğer rezervasyon yoksa hata döndür
        if (!reservations.Any())
        {
            var error = new ErrorResult(new List<string> { "Bu kullanıcı için rezervasyon bulunamadı" });
            return ApiResult<IEnumerable<ReservationDTO>>.FailureResult(error, HttpStatusCode.NotFound);
        }

        // Rezervasyonları DTO'ya dönüştür
        var reservationDtos = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);
        return ApiResult<IEnumerable<ReservationDTO>>.SuccessResult(reservationDtos);
    }

    /// <summary>
    /// Belirtilen oda GUID'ine göre rezervasyonları alır.
    /// </summary>
    /// <param name="roomGuid">Odanın GUID'i.</param>
    /// <returns>Odaya ait rezervasyonların DTO'ları.</returns>
    public async Task<ApiResult<IEnumerable<ReservationDTO>>> GetReservationsByRoomAsync(Guid roomGuid)
    {
        // Odaya ait rezervasyonları al
        var reservations = await _uow.ReservationRepository.GetAllAsync(r => r.GUID == roomGuid);

        // Eğer rezervasyon yoksa hata döndür
        if (!reservations.Any())
        {
            var error = new ErrorResult(new List<string> { "Bu oda için rezervasyon bulunamadı" });
            return ApiResult<IEnumerable<ReservationDTO>>.FailureResult(error, HttpStatusCode.NotFound);
        }

        // Rezervasyonları DTO'ya dönüştür
        var reservationDtos = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);
        return ApiResult<IEnumerable<ReservationDTO>>.SuccessResult(reservationDtos);
    }

    /// <summary>
    /// Yeni bir rezervasyon ekler.
    /// </summary>
    /// <param name="reservationDto">Eklenecek rezervasyonun DTO'su.</param>
    /// <returns>Eklenen rezervasyonun DTO'su.</returns>
    public async Task<ApiResult<ReservationDTO>> AddReservationAsync(ReservationAddRequestDTO reservationDto)
    {
        // Odayı GUID ile bul
        var room = await _uow.RoomRepository.GetAsync(r => r.GUID == reservationDto.RoomGuid);
        
        // Oda uygunluk kontrolü
        var isRoomAvailable = await _uow.ReservationRepository.GetAllAsync(r =>
            r.GUID == reservationDto.RoomGuid &&
            (reservationDto.StartDate < r.CheckOutDate && reservationDto.EndDate > r.CheckInDate));

        // Eğer oda uygun değilse hata fırlat
        if (isRoomAvailable.Any())
        {
            throw new RoomNotAvailableException(room.ID, reservationDto.StartDate.ToShortDateString(), reservationDto.EndDate.ToShortDateString());
        }

        // Rezervasyonu domain nesnesine dönüştür
        var reservation = _mapper.Map<Domain.Entities.Reservation>(reservationDto);

        // Rezervasyonu ekle ve değişiklikleri kaydet
        await _uow.ReservationRepository.AddAsync(reservation);
        await _uow.SaveAsync();

        // Eklenen rezervasyonu DTO'ya dönüştür
        var reservationDtoResult = _mapper.Map<ReservationDTO>(reservation);
        return ApiResult<ReservationDTO>.SuccessResult(reservationDtoResult);
    }

    /// <summary>
    /// Belirtilen rezervasyonu iptal eder.
    /// </summary>
    /// <param name="reservationGuid">İptal edilecek rezervasyonun GUID'i.</param>
    /// <returns>İptal işleminin sonucu.</returns>
    public async Task<ApiResult<bool>> CancelReservationAsync(Guid reservationGuid)
    {
        // Rezervasyonu GUID ile bul
        var reservation = await _uow.ReservationRepository.GetAsync(r => r.GUID == reservationGuid);

        // Rezervasyon bulunamazsa hata döndür
        if (reservation == null)
        {
            var error = new ErrorResult(new List<string> { "İptal edilecek rezervasyon bulunamadı" });
            return ApiResult<bool>.FailureResult(error, HttpStatusCode.NotFound);
        }

        // Rezervasyonu sil ve değişiklikleri kaydet
        _uow.ReservationRepository.Remove(reservation);
        await _uow.SaveAsync();

        // İptal işlemi başarılı
        return ApiResult<bool>.SuccessResult(true);
    }
}