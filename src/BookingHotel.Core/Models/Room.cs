using System.ComponentModel.DataAnnotations;

namespace BackendAPIBookingHotel.Model
{
	public class Room
	{
		[Key]
		public int RoomID { get; set; }
		public int HotelID { get; set; }
		public string RoomNumber { get; set; }
		public int RoomSquare { get; set; }
		public bool IsActive { get; set; }

		public Hotel Hotel { get; set; }
		public ICollection<Booking> Bookings { get; set; }
		public virtual ICollection<BedRoom> BedRooms { get; set; }
        public virtual ICollection<ImageRooms> ImageRooms { get; set; }
		public virtual RoomDetail RoomDetail { get; set; }	
	}


}
