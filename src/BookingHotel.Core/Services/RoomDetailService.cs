using BackendAPIBookingHotel.Model;
using BookingHotel.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingHotel.Core.Services
{
    public class RoomDetailService : IRoomDetailService
    {

        private readonly IUnitOfWork _unitOfWork;

        public RoomDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<RetureReponse> DeleteRoomDetail(int idRoomDetail)
        {
            var reponse = new RetureReponse();
            await _unitOfWork.Repository<RoomDetail>().DeleteAsync(idRoomDetail);
            await _unitOfWork.SaveChangesAsync();
            reponse.returnCode = 200;
            reponse.returnMessage = "Xóa Phòng khách sạn thành công";
            return reponse;
        }

        public async Task<List<RoomDetail>> getAll()
        {
            var list = new List<RoomDetail>();
            list = (List<RoomDetail>)await _unitOfWork.Repository<RoomDetail>().GetAllAsync();
            return list;
        }

        public Task<RoomDetail> getRoomDetailById(int id)
        {
            var room = _unitOfWork.Repository<RoomDetail>().GetByIdAsync(id);
            return room;
        }

        public async Task<RetureReponse> InsertRoomDetail(RoomDetailDTO roomDetailDTO)
        {
            var reponse = new RetureReponse();

            var roomDetail = new RoomDetail()
            {
                
            };
            await _unitOfWork.Repository<Room>().AddAsync(room);
            await _unitOfWork.SaveChangesAsync();
            reponse.returnCode = 200;
            reponse.returnMessage = "Thêm Phòng khách sạn thành công";
            return reponse;
        }

        public Task<RetureReponse> UpdateRoomDetail(int idRoomDetail, RoomDetailDTO roomDetailDTO)
        {
            throw new NotImplementedException();
        }
    }
}
