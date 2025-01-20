namespace HotelReservation.Application.DTO.Login;

public class LoginResponseDTO
{
    public Guid GUID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? GroupID { get; set; }
    public string GroupName { get; set; }
    public string Token { get; set; }
}