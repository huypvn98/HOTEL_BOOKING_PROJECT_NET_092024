using BackendAPIBookingHotel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotel.Core.DTO
{
    public class RoomDetailDTO
    {
           public int RoomId       {get;set;}
           public string RoomFittings {get;set;}
           public string RoomView     {get;set;}
           public string RoomType     {get;set;}
           public double PricePerNight{get;set;}
           public bool IsAvailable { get; set; } = true;
    }
}
