namespace BackendAPIBookingHotel.Model
{
	public class Admin
	{
		public int StaffID{get;set;}
		public string Position{get;set;}
		public DateTime? AssignedDate{get;set;}
		public string AdminSpecificInfo{ get; set; }

		public Person Person { get;set;}
	}
}
