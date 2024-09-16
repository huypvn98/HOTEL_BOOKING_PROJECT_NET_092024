namespace BackendAPIBookingHotel.Model
{
	public class Role
	{
		public int RoleID { get; set; }

		public string RoleName { get; set; }

		public string Description { get; set; }

		public ICollection<UserRole> UserRoles { get; set; }
	}	
}
