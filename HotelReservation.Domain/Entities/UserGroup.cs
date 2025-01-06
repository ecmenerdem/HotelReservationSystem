using HotelReservation.Domain.Entities.Base;

namespace HotelReservation.Domain.Entities;

public class UserGroup : AuditableEntity
{
    public UserGroup()
    {
        Users = new HashSet<User>();
    }

    public string Name { get; set; }
    public virtual IEnumerable<User> Users { get; set; }
}