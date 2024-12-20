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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IGenericValidator _validator;

        public HotelManager(IUnitOfWork uow, IMapper mapper, IGenericValidator validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResult<HotelDTO>> GetHotelByIdAsync(int hotelId)
        {
            var hotel = await _uow.HotelRepository.GetAsync(q => q.ID == hotelId);

            if (hotel is null)
            {
                var error = new ErrorResult(new List<string>() { "Otel Bulunamadı" });
                return ApiResult<HotelDTO>.FailureResult(error, HttpStatusCode.NotFound);
                
            }
            var hotelDto = _mapper.Map<HotelDTO>(hotel);
            return ApiResult<HotelDTO>.SuccessResult(hotelDto);
        }

        public async Task<ApiResult<IEnumerable<HotelDTO>>> GetAllHotelsAsync()
        {
            var hotels = await _uow.HotelRepository.GetAllAsync();
    
            if (!hotels.Any())
            {
                var error = new ErrorResult(new List<string> { "Hiç otel bulunamadı" });
                return ApiResult<IEnumerable<HotelDTO>>.FailureResult(error, HttpStatusCode.NotFound);
            }

            var hotelDtos = _mapper.Map<IEnumerable<HotelDTO>>(hotels);
            return ApiResult<IEnumerable<HotelDTO>>.SuccessResult(hotelDtos);
        }

        public async Task<ApiResult<HotelDTO>> AddHotelAsync(HotelAddRequestDTO hotelDto)
        {
            await _validator.ValidateAsync(hotelDto, typeof(HotelAddValidator));
            var hotel = _mapper.Map<Domain.Entities.Hotel>(hotelDto);
            await _uow.HotelRepository.AddAsync(hotel);
            await _uow.SaveAsync();

            var hotelDtoResult = _mapper.Map<HotelDTO>(hotel);
            return ApiResult<HotelDTO>.SuccessResult(hotelDtoResult);
        }

        public async Task<ApiResult<bool>> UpdateHotelAsync(HotelUpdateRequestDTO hotelDto)
        {
            await _validator.ValidateAsync(hotelDto, typeof(HotelUpdateValidator));

            var hotel = await _uow.HotelRepository.GetAsync(h => h.GUID == hotelDto.GUID);
            if (hotel is null)
            {
                var error = new ErrorResult(new List<string> { "Güncellenecek otel bulunamadı" });
                return ApiResult<bool>.FailureResult(error, HttpStatusCode.NotFound);
            }

            _mapper.Map(hotelDto, hotel);
            _uow.HotelRepository.Update(hotel);
            await _uow.SaveAsync();

            return ApiResult<bool>.SuccessResult(true);
        }

        public async Task<ApiResult<bool>> DeleteHotelAsync(int hotelId)
        {
            var hotel = await _uow.HotelRepository.GetAsync(h => h.ID == hotelId);
            if (hotel is null)
            {
                var error = new ErrorResult(new List<string> { "Silinecek otel bulunamadı" });
                return ApiResult<bool>.FailureResult(error, HttpStatusCode.NotFound);
            }

            hotel.IsActive = false;
            hotel.IsDeleted = true;
            _uow.HotelRepository.Update(hotel);
            await _uow.SaveAsync();

            return ApiResult<bool>.SuccessResult(true);
        }
    }
}
