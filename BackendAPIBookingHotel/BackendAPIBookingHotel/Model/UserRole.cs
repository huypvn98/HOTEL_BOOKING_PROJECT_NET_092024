namespace BackendAPIBookingHotel.Model
{
	public class UserRole
	{
		public int UserRoleId { get; set; }

		public int UserID { get; set; }

		public int RoleID { get; set; }

		public User User { get; set; }

		public Role Role { get; set; } 

	}
}
