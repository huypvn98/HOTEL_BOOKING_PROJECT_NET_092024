namespace BackendAPIBookingHotel.Model
{
	public class Booking
	{

		public int BookingID {get ; set ;}


		public int RoomID { get; set; }
		public int CustomerID {get ; set ;}
		public int DepositID {get ; set ;}
		
		public DateTime BookingDate {get ; set ;}
		public DateTime CheckInDate {get ; set ;}
		public DateTime CheckOutDate {get ; set ;}
		public DateTime BookingStatus {get ; set ;}
		public int NumberOfDays { get; set; }


		public Room Room { get; set; }
		public Customer Customer { get; set; }
		public ICollection<BookingDetail> BookingDetails { get; set; }
		public ICollection<Deposit> Deposits { get; set; }
		public ICollection<Invoice> Invoices { get; set; }

	}
}
