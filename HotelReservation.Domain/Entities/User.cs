﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservation.Domain.Entities.Base;

namespace HotelReservation.Domain.Entities
{
    public class User : AuditableEntity
    {
        public User()
        {
            Reservations = new HashSet<Reservation>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? GroupID { get; set; }

        // Navigation Property
        public virtual IEnumerable<Reservation> Reservations { get; set; }
        public virtual UserGroup Group { get; set; }


        // Kullanıcının tam adını almak için yardımcı bir property
        public string FullName => $"{FirstName} {LastName}";
    }
}
