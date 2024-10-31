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

        public async Task<ApiResult<UserDTO>> AddUserAsync(UserAddRequestDTO userRequestDto)
        {
            await _validator.ValidateAsync(userRequestDto, typeof(UserRegisterValidator));
            // Kullanıcı şifresini hashle
            var hashedPassword = _passwordHasher.HashPassword(userRequestDto.Password);
            Domain.Entities.User user = _mapper.Map<Domain.Entities.User>(userRequestDto);
            user.Password = hashedPassword;
            await _uow.UserRepository.AddAsync(user);
            await _uow.SaveAsync();
            
            UserDTO userDto = _mapper.Map<UserDTO>(user);
            userDto.Password = null;
            return ApiResult<UserDTO>.SuccessResult(userDto);
        }

        public Task<ApiResult<bool>> DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<UserDTO>> GetUserByEMailAsync(string eMailAddress)
        {
            var user = await _uow.UserRepository.GetAsync(q => q.Email.ToLower()==eMailAddress.ToLower());
            
            if (user == null)
            {
                var error = new ErrorResult(new List<string>(){"Kullanıcı Bulunamadı"});
                return ApiResult<UserDTO>.FailureResult(error, HttpStatusCode.NotFound);
            }

            return  ApiResult<UserDTO>.SuccessResult(_mapper.Map<UserDTO>(user));
        }

        public async Task<ApiResult<IEnumerable<UserDTO>>> GetAllUsersAsync()
        {
            var users = await _uow.UserRepository.GetAllAsync();
            return ApiResult<IEnumerable<UserDTO>>.SuccessResult(users.Select(user => _mapper.Map<UserDTO>(user)).ToList());
        }

        public async Task<ApiResult<UserDTO>> GetUserByGUIDAsync(Guid userGUID)
        {
            var user = await _uow.UserRepository.GetAsync(q => q.GUID == userGUID);

            if (user == null)
            {
                var error = new ErrorResult(new List<string>(){"Kullanıcı Bulunamadı"});
                
                return ApiResult<UserDTO>.FailureResult(error, HttpStatusCode.NotFound);
            }
            
            return  ApiResult<UserDTO>.SuccessResult(_mapper.Map<UserDTO>(user));
        }

        public async Task<ApiResult<UserDTO>> GetUserByIdAsync(int userId)
        {
            var user = await _uow.UserRepository.GetAsync(q => q.ID == userId);
            
            if (user == null)
            {
                var error = new ErrorResult(new List<string>(){"Kullanıcı Bulunamadı"});
                
                return ApiResult<UserDTO>.FailureResult(error, HttpStatusCode.NotFound);
            }
            
            return  ApiResult<UserDTO>.SuccessResult(_mapper.Map<UserDTO>(user));
            
        }

        public async Task<ApiResult<UserDTO>> GetUserByUsernameAsync(string username)
        {
            var user = await _uow.UserRepository.GetAsync(q => q.Username == username);
            if (user == null)
            {
                var error = new ErrorResult(new List<string>(){"Kullanıcı Bulunamadı"});
                
                return ApiResult<UserDTO>.FailureResult(error, HttpStatusCode.NotFound);
            }
            
            return  ApiResult<UserDTO>.SuccessResult(_mapper.Map<UserDTO>(user));
        }

        public async Task<ApiResult<LoginResponseDTO>> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            await _validator.ValidateAsync(loginRequestDTO, typeof(LoginValidator));
            var user = await _uow.UserRepository.GetAsync(x => x.Username == loginRequestDTO.KullaniciAdi);

            if (user == null || !_passwordHasher.VerifyPassword(user.Password, loginRequestDTO.Sifre))
            {
                throw new InvalidUserCredentialsException();
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

        public async Task<ApiResult<bool>> UpdateUserAsync(UserUpdateRequestDTO userDto)
        {
            var user = await _uow.UserRepository.GetAsync(q => q.GUID == userDto.Guid);
            if (user == null)
            {
                var error = new ErrorResult(new List<string>(){"Kullanıcı Bulunamadı"});
                
                return ApiResult<bool>.FailureResult(error, HttpStatusCode.NotFound);
            }
            
            _mapper.Map(userDto, user);
            _uow.UserRepository.Update(user);
            await _uow.SaveAsync();
            
            return ApiResult<bool>.SuccessResult(true);
        }
    }
}