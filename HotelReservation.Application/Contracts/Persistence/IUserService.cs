using HotelReservation.Application.DTO.User;
using HotelReservation.Application.Result;

namespace HotelReservation.Application.Contracts.Persistence
{
    public interface IUserService
    {
        Task<ApiResult<UserDTO>> GetUserByIdAsync(int userId);
        Task<ApiResult<UserDTO>> GetUserByGUIDAsync(Guid userGUID);
        Task<ApiResult<UserDTO>> GetUserByUsernameAsync(string username);
        Task<ApiResult<UserDTO>> GetUserByEMailAsync(string eMailAddress);
        Task<ApiResult<IEnumerable<UserDTO>>> GetAllUsersAsync();
        Task<ApiResult<UserDTO>> AddUserAsync(UserAddRequestDTO userDto);
        Task<ApiResult<bool>> UpdateUserAsync(UserUpdateRequestDTO userDto);
        Task<ApiResult<bool>> DeleteUserAsync(int userId);
        Task<ApiResult<LoginResponseDTO>> LoginAsync(LoginRequestDTO loginRequestDTO);
    }
}
