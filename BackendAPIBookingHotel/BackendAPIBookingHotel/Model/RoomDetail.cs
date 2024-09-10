namespace BackendAPIBookingHotel.Model
{
	public class RoomDetail
	{
		public int RoomDetailID { get; set; }
		public int RoomID{get;set;}
		public string RoomFittings{get;set;}
		public string RoomView{get;set;}
		public string RoomType{get;set;}
		public int PricePerNight{get;set;}
		public int IsAvailable{ get; set; }

		public Room Room { get; set; }
	}
}
