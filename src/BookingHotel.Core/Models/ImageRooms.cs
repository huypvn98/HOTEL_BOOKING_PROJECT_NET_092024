using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPIBookingHotel.Model
{
    public class ImageRooms
    {
        [Key]
        public int Id { get; set; }

        public string NameFileImg { get; set; }

        [ForeignKey("RoomID")]
        public virtual Room Room { get; set; }
    }
}