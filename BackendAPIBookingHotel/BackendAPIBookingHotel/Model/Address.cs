namespace BackendAPIBookingHotel.Model
{
	public class Address
	{
		public int AddressID { get; set; }

		public int? PersonID { get; set; }

		public string StreetAddress { get; set; }

		public string City { get; set; }


		public string Country { get; set; }


		public string AddressType { get; set; }

		public bool IsPrimary { get; set; }

		public Person Persons { get; set; }

	}
}
