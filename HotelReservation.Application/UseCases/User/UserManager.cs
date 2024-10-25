using AutoMapper;
using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.Contracts.Security;
using HotelReservation.Application.DTO.User;
using HotelReservation.Domain.Exceptions;
using HotelReservation.Domain.Repositories.DataManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Application.Contracts.Validation;
using HotelReservation.Application.UseCases.User.Validation;
using HotelReservation.Domain.Repositories;

namespace HotelReservation.Application.UseCases.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IGenericValidator _validator;
        public UserManager(IUnitOfWork uow, IPasswordHasher passwordHasher, IMapper mapper, ITokenService tokenService, IGenericValidator validator)
        {
            _uow = uow;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _tokenService = tokenService;
            _validator = validator;
        }

        public async Task AddUserAsync(UserAddRequestDTO userDto)
        {
            await _validator.ValidateAsync(userDto,typeof(UserRegisterValidator));
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUserByGUIDAsync(int userGUID)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            var user = await _uow.UserRepository.GetAsync(x => 
                x.Username == loginRequestDTO.KullaniciAdi && x.Password == _passwordHasher.HashPassword(loginRequestDTO.Sifre));

            if (user is null)
            {
                throw new UserNotFoundException();
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim("KullaniciGUID", user.GUID.ToString()),
                    new Claim("KullaniciAdi", user.Username)
                };
                var token = _tokenService.GenerateToken(claims);
                var loginResponseDTO=_mapper.Map<LoginResponseDTO>(user);
                loginResponseDTO.Token = token;
                return loginResponseDTO;
            }
        }

        public Task UpdateUserAsync(UserUpdateRequestDTO userDto)
        {
            throw new NotImplementedException();
        }
    }
}
