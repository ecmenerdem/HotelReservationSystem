using AutoMapper;
using HotelReservation.Application.Contracts.Persistence;
using HotelReservation.Application.Contracts.Security;
using HotelReservation.Application.DTO.User;
using HotelReservation.Domain.Exceptions;
using HotelReservation.Domain.Repositories.DataManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Application.UseCases.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserManager(IUnitOfWork uow, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _uow = uow;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public Task AddUserAsync(UserAddRequestDTO userDto)
        {
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

        public async Task<UserDTO> LoginAsync(string username, string password)
        {
            var user = await _uow.UserRepository.GetAsync(x => x.Username == username && x.Password == _passwordHasher.HashPassword(password));

            if (user is null)
            {
                throw new UserNotFoundException();
            }
            else
            {
                return _mapper.Map<UserDTO>(user);
            }
        }

        public Task UpdateUserAsync(UserUpdateRequestDTO userDto)
        {
            throw new NotImplementedException();
        }
    }
}
