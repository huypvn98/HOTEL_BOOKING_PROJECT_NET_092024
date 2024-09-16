namespace BackendAPIBookingHotel.Model
{
	public class User
	{
		public int UserID { get; set; }

        public string Username{ get; set; }

        public string PasswordHash{ get; set; }
		public string PasswordSalt { get; set; }

		public DateTime CreateDate { get; set; }

		public Person Person { get; set; }

		public ICollection<UserRole> UserRoles { get; set; }

	}
}
