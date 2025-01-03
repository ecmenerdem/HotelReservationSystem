﻿using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Application.DTO.User
{
    public class UserAddRequestDTO
    {
        [Required(ErrorMessage = "İsim Zorunludur.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
