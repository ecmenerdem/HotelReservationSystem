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
            throw new NotImplementedException();
        }

        public async Task<HotelDTO> AddHotelAsync(HotelAddRequestDTO hotelDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateHotelAsync(HotelUpdateRequestDTO hotelDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteHotelAsync(int hotelId)
        {
            throw new NotImplementedException();
        }
    }
}
