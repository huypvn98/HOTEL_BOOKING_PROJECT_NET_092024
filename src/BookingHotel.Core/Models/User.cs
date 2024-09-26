namespace BackendAPIBookingHotel.Model
{
	public class User
	{
		public int UserID { get; set; }

        public string Username{ get; set; }

        public string PasswordHash{ get; set; }
		public string PasswordSalt { get; set; }

		public DateTime CreateDate { get; set; }

		public Staff Staff { get; set; }
		public Admin Admin { get; set; }
		public Customer Customer { get; set; }

		public ICollection<UserRole> UserRoles { get; set; }

	}
}
