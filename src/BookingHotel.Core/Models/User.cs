using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendAPIBookingHotel.Model{
public class User
{
    [Key]
    public int UserID { get; set; } // Không tự động tăng

    public string Username { get; set; }
    
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }

    public DateTime CreateDate { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}

}
