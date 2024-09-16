namespace BackendAPIBookingHotel.Model
{
	public class Hotel
	{
		public int HotelID { get; set; }

		public string HotelName { get; set; }

		public string Description { get; set; }

		public DateTime CreatedDate { get; set; }


		public ICollection<Room> Rooms { get; set; }

		public ICollection<Staff> Staffs { get; set; }
	}
}
