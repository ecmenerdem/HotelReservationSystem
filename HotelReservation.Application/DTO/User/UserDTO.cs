using System.Text.Json.Serialization;

namespace HotelReservation.Application.DTO.User
{
    public class UserDTO
    {
        public Guid GUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
