﻿using HotelReservation.Application.DTO.User;
using HotelReservation.Application.Result;

namespace HotelReservation.Application.Contracts.Persistence
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task<UserDTO> GetUserByGUIDAsync(Guid userGUID);
        Task<UserDTO> GetUserByUsernameAsync(string username);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task AddUserAsync(UserAddRequestDTO userDto);
        Task UpdateUserAsync(UserUpdateRequestDTO userDto);
        Task DeleteUserAsync(int userId);
        Task<ApiResult<LoginResponseDTO>> LoginAsync(LoginRequestDTO loginRequestDTO);
    }
}
