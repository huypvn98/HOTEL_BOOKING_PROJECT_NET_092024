using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPIBookingHotel.Model
{
    public class BedRoom
    {
        [Key, Column(Order = 0)]
        public int RoomID { get; set; }

        [Key, Column(Order = 1)]
        public int BedID { get; set; }
         
        public int Quantity { get; set; }
    }
}