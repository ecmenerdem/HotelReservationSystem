using System.Net;
using AutoMapper;
using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.Contracts.Security;
using HotelReservation.Application.DTO.User;
using HotelReservation.Domain.Exceptions;
using HotelReservation.Domain.Repositories.DataManagement;
using System.Security.Claims;
using HotelReservation.Application.Contracts.Validation;
using HotelReservation.Application.Result;
using HotelReservation.Application.UseCases.User.Validation;

namespace HotelReservation.Application.UseCases.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IGenericValidator _validator;

        public UserManager(IUnitOfWork uow, IPasswordHasher passwordHasher, IMapper mapper, ITokenService tokenService,
            IGenericValidator validator)
        {
            _uow = uow;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _tokenService = tokenService;
            _validator = validator;
        }

        public async Task AddUserAsync(UserAddRequestDTO userDto)
        {
            await _validator.ValidateAsync(userDto, typeof(UserRegisterValidator));
            // Kullanıcı şifresini hashle
            var hashedPassword = _passwordHasher.HashPassword(userDto.Password);
            Domain.Entities.User user = _mapper.Map<Domain.Entities.User>(userDto);
            user.Password = hashedPassword;
            await _uow.UserRepository.AddAsync(user);
            await _uow.SaveAsync();
        }

        public Task DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _uow.UserRepository.GetAllAsync();
            return users.Select(user => _mapper.Map<UserDTO>(user)).ToList();
        }

        public async Task<UserDTO> GetUserByGUIDAsync(Guid userGUID)
        {
            var user = await _uow.UserRepository.GetAsync(q => q.GUID == userGUID);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByIdAsync(int userId)
        {
            var user = await _uow.UserRepository.GetAsync(q => q.ID == userId);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByUsernameAsync(string username)
        {
            var user = await _uow.UserRepository.GetAsync(q => q.Username == username);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<ApiResult<LoginResponseDTO>> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            var user = await _uow.UserRepository.GetAsync(x => x.Username == loginRequestDTO.KullaniciAdi);

            if (user == null || !_passwordHasher.VerifyPassword(user.Password, loginRequestDTO.Sifre))
            {
                // Kullanıcı bulunamadı veya şifre yanlış

                List<string> errors = new() { "Kullanıcı Adı Veya Şifre Yanlış" };
                
                return ApiResult<LoginResponseDTO>.FailureResult(new ErrorResult(errors), HttpStatusCode.NotFound);
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim("KullaniciGUID", user.GUID.ToString()),
                    new Claim("KullaniciAdi", user.Username)
                };
                var token = _tokenService.GenerateToken(claims);
                var loginResponseDTO = _mapper.Map<LoginResponseDTO>(user);
                loginResponseDTO.Token = token;
                return ApiResult<LoginResponseDTO>.SuccessResult(loginResponseDTO);
            }
        }

        public async Task UpdateUserAsync(UserUpdateRequestDTO userDto)
        {
            var user = await _uow.UserRepository.GetAsync(q => q.GUID == userDto.Guid);
            if (user is null)
            {
                throw new UserNotFoundException();
            }

            _mapper.Map(userDto, user);
            _uow.UserRepository.Update(user);
            await _uow.SaveAsync();
        }
    }
}