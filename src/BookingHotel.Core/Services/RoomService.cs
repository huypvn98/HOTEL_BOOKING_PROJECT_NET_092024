using BackendAPIBookingHotel.Model;
using BookingHotel.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotel.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<RetureReponse> DeleteRoom(int idRoom)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> getAll()
        {
            throw new NotImplementedException();
        }

        public async Task<RetureReponse> InsertRoom(RoomDTO roomDTO)
        {
            var reponse = new RetureReponse();
           
          var room = new Room()
          {
              HotelID = roomDTO.hotelID,
              RoomNumber = roomDTO.roomNumber,
              RoomSquare = roomDTO.roomSquare,
              IsActive = roomDTO.isActive
          };
          await _unitOfWork.Repository<Room>().AddAsync(room);
          await _unitOfWork.SaveChangesAsync();
          reponse.returnCode = 200;
          reponse.returnMessage = "Thêm Phòng khách sạn thành công";
            return reponse;
          
        }

        public Task<RetureReponse> UpdateRoom(int idRoom, RoomDTO roomDTO)
        {
            throw new NotImplementedException();
        }
    }
}
