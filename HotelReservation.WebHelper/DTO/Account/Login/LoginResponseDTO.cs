namespace HotelReservation.WebHelper.DTO.Account.Login;

public class LoginResponseDTO
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
}