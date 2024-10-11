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
        public async Task<RetureReponse> DeleteRoom(int idRoom)
        {
            var reponse = new RetureReponse();
             await _unitOfWork.Repository<Room>().DeleteAsync(idRoom);
            await _unitOfWork.SaveChangesAsync();
            reponse.returnCode = 200;
            reponse.returnMessage = "Xóa Phòng khách sạn thành công";
            return reponse;
        }

        public async Task<List<Room>> getAll()
        {
            var list = new List<Room>();
            list= (List<Room>) await _unitOfWork.Repository<Room>().GetAllAsync();
            return list;
        }

        public Task<Room> getRoomById(int id)
        {
            var room= _unitOfWork.Repository<Room>().GetByIdAsync(id);
            return room;
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

        public async Task<RetureReponse> UpdateRoom(int idRoom, RoomDTO roomDTO)
        {
            var reponse = new RetureReponse();
            var room= await _unitOfWork.Repository<Room>().GetByIdAsync(idRoom);
            room.RoomNumber = roomDTO.roomNumber;
            room.RoomSquare = roomDTO.roomSquare;
            room.IsActive = roomDTO.isActive;
            room.HotelID = roomDTO.hotelID;
            await _unitOfWork.SaveChangesAsync();
            reponse.returnCode = 200;
            reponse.returnMessage = "Cập nhật phòng khách sạn thành công";
            return reponse;
        }
    }
}
