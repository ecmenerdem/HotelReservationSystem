using HotelReservation.Application.DTO.User;

namespace HotelReservation.Application.Contracts
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int userId);
        Task<UserDTO> GetUserByGUIDAsync(int userGUID);
        Task<UserDTO> GetUserByUsernameAsync(string username);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task AddUserAsync(UserAddRequestDTO userDto);
        Task UpdateUserAsync(UserUpdateRequestDTO userDto);
        Task DeleteUserAsync(int userId);
        Task<UserDTO> LoginAsync(string username, string password);
    }
}
