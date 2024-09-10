namespace BackendAPIBookingHotel.Model
{
	public class Room
	{
		public int RoomID { get; set; }
		public int HotelID { get; set; }
		public string RoomNumber { get; set; }
		public int RoomSquare { get; set; }
		public int IsActive { get; set; }

		public Hotel Hotel { get; set; }

		public ICollection<Booking> Bookings { get; set; }
		public ICollection<RoomDetail> RoomDetails { get; set; }
	}


}
