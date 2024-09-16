namespace BackendAPIBookingHotel.Model
{
	public class Staff
	{

		public int StaffID{get; set;}
		public int Position{get; set;}
		public string HireDate{get; set;}
		public int HotelID { get; set; }

		public Hotel Hotel { get; set; }

		public Person Person { get; set; }
	}
}
