﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotel.Core.DTO
{
    public class RoomDTO
    {
        public int hotelID { get; set; }
        public string roomNumber { get; set; }
        public int roomSquare { get; set; }
        public bool isActive { get; set; } = true;
    }
}