using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.Contracts.Security;
using HotelReservation.Application.Contracts.Validation;
using HotelReservation.Application.DTO.Hotel;
using HotelReservation.Application.DTO.User;
using HotelReservation.Application.Result;
using HotelReservation.Application.UseCases.Hotel.Validation;
using HotelReservation.Domain.Repositories.DataManagement;

namespace HotelReservation.Application.UseCases.Hotel
{
    public class HotelManager: IHotelService
    {
        private readonly IUnitOfWork _uow; // Birim iş birimi, veri erişim işlemleri için kullanılır.
        private readonly IMapper _mapper; // DTO'lar ile domain nesneleri arasında dönüşüm yapmak için kullanılır.
        private readonly IGenericValidator _validator; // Giriş verilerini doğrulamak için kullanılır.

        public HotelManager(IUnitOfWork uow, IMapper mapper, IGenericValidator validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Belirtilen otel ID'sine göre oteli alır.
        /// </summary>
        /// <param name="hotelId">Otelin ID'si.</param>
        /// <returns>Otelin DTO'su.</returns>
        public async Task<ApiResult<HotelDTO>> GetHotelByIdAsync(int hotelId)
        {
            // Oteli ID ile bul
            var hotel = await _uow.HotelRepository.GetAsync(q => q.ID == hotelId);

            // Otel bulunamazsa hata döndür
            if (hotel is null)
            {
                var error = new ErrorResult(new List<string>() { "Otel Bulunamadı" });
                return ApiResult<HotelDTO>.FailureResult(error, HttpStatusCode.NotFound);
            }

            // Oteli DTO'ya dönüştür
            var hotelDto = _mapper.Map<HotelDTO>(hotel);
            return ApiResult<HotelDTO>.SuccessResult(hotelDto);
        }

        /// <summary>
        /// Tüm otelleri alır.
        /// </summary>
        /// <returns>Tüm otellerin DTO'ları.</returns>
        public async Task<ApiResult<IEnumerable<HotelDTO>>> GetAllHotelsAsync()
        {
            // Tüm otelleri al
            var hotels = await _uow.HotelRepository.GetAllAsync();
    
            // Eğer otel yoksa hata döndür
            if (!hotels.Any())
            {
                var error = new ErrorResult(new List<string> { "Hiç otel bulunamadı" });
                return ApiResult<IEnumerable<HotelDTO>>.FailureResult(error, HttpStatusCode.NotFound);
            }

            // Otelleri DTO'ya dönüştür
            var hotelDtos = _mapper.Map<IEnumerable<HotelDTO>>(hotels);
            return ApiResult<IEnumerable<HotelDTO>>.SuccessResult(hotelDtos);
        }

        /// <summary>
        /// Yeni bir otel ekler.
        /// </summary>
        /// <param name="hotelDto">Eklenecek otelin DTO'su.</param>
        /// <returns>Eklenen otelin DTO'su.</returns>
        public async Task<ApiResult<HotelDTO>> AddHotelAsync(HotelAddRequestDTO hotelDto)
        {
            // Otel DTO'sunu doğrula
            await _validator.ValidateAsync(hotelDto, typeof(HotelAddValidator));
            
            // Oteli domain nesnesine dönüştür
            var hotel = _mapper.Map<Domain.Entities.Hotel>(hotelDto);
            
            // Oteli ekle ve değişiklikleri kaydet
            await _uow.HotelRepository.AddAsync(hotel);
            await _uow.SaveAsync();

            // Eklenen oteli DTO'ya dönüştür
            var hotelDtoResult = _mapper.Map<HotelDTO>(hotel);
            return ApiResult<HotelDTO>.SuccessResult(hotelDtoResult);
        }

        /// <summary>
        /// Belirtilen oteli günceller.
        /// </summary>
        /// <param name="hotelDto">Güncellenecek otelin DTO'su.</param>
        /// <returns>Güncelleme işleminin sonucu.</returns>
        public async Task<ApiResult<bool>> UpdateHotelAsync(HotelUpdateRequestDTO hotelDto)
        {
            // Otel DTO'sunu doğrula
            await _validator.ValidateAsync(hotelDto, typeof(HotelUpdateValidator));

            // Oteli GUID ile bul
            var hotel = await _uow.HotelRepository.GetAsync(h => h.GUID == hotelDto.GUID);
            // Otel bulunamazsa hata döndür
            if (hotel is null)
            {
                var error = new ErrorResult(new List<string> { "Güncellenecek otel bulunamadı" });
                return ApiResult<bool>.FailureResult(error, HttpStatusCode.NotFound);
            }

            // Otel DTO'sunu mevcut otel nesnesine harita
            _mapper.Map(hotelDto, hotel);
            // Oteli güncelle ve değişiklikleri kaydet
            _uow.HotelRepository.Update(hotel);
            await _uow.SaveAsync();

            // Güncelleme işlemi başarılı
            return ApiResult<bool>.SuccessResult(true);
        }

        /// <summary>
        /// Belirtilen oteli siler.
        /// </summary>
        /// <param name="hotelId">Silinecek otelin ID'si.</param>
        /// <returns>Silme işleminin sonucu.</returns>
        public async Task<ApiResult<bool>> DeleteHotelAsync(int hotelId)
        {
            // Oteli ID ile bul
            var hotel = await _uow.HotelRepository.GetAsync(h => h.ID == hotelId);
            // Otel bulunamazsa hata döndür
            if (hotel is null)
            {
                var error = new ErrorResult(new List<string> { "Silinecek otel bulunamadı" });
                return ApiResult<bool>.FailureResult(error, HttpStatusCode.NotFound);
            }

            // Oteli pasif hale getir ve silindi olarak işaretle
            hotel.IsActive = false;
            hotel.IsDeleted = true;
            _uow.HotelRepository.Update(hotel);
            await _uow.SaveAsync();

            // Silme işlemi başarılı
            return ApiResult<bool>.SuccessResult(true);
        }
    }
}