﻿using BackendAPIBookingHotel.Model;
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
              IsActive = roomDTO.isActive,
              RoomDetailID=roomDTO.iddetail,
              
          };
          await _unitOfWork.Repository<Room>().AddAsync(room);
          await _unitOfWork.SaveChangesAsync();
            // add bed
            var bedRoom= new BedRoom();

            bedRoom.RoomID=room.RoomID;
            bedRoom.BedID=roomDTO.idBed;
            bedRoom.Quantity=roomDTO.quantity;
            await _unitOfWork.Repository<BedRoom>().AddAsync(bedRoom);
            await _unitOfWork.SaveChangesAsync();


            //add image
            var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }
            //check folder image exci
            var imgRooms = new List<ImageRooms>();

            foreach (var image in roomDTO.Images)
            {
                if (image.Length > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + image.FileName;
                   
                    var filePath = Path.Combine(imageDirectory,fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await image.CopyToAsync(stream);
                    }
                    var imgRoom = new ImageRooms();
                    imgRoom.NameFileImg = fileName;
                    imgRoom.RoomID = room.RoomID;
                    imgRooms.Add(imgRoom);
                }

            }
            await _unitOfWork.Repository<ImageRooms>().AddListAsync(imgRooms);
            await _unitOfWork.SaveChangesAsync();
          reponse.returnCode = 200;
          reponse.returnMessage = "Thêm Phòng khách sạn thành công";
         return reponse;
          
        }

        public async Task<RetureReponse> UpdateRoom(int idRoom, RoomDTO roomDTO)
        {
            //var reponse = new RetureReponse();
            //var room= await _unitOfWork.Repository<Room>().GetByIdAsync(idRoom);
            //room.RoomNumber = roomDTO.roomNumber;
            //room.RoomSquare = roomDTO.roomSquare;
            //room.IsActive = roomDTO.isActive;
            //room.HotelID = roomDTO.hotelID;
            //await _unitOfWork.SaveChangesAsync();
            //reponse.returnCode = 200;
            //reponse.returnMessage = "Cập nhật phòng khách sạn thành công";
            //return reponse;
            var reponse = new RetureReponse();
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(idRoom);

            room.HotelID = roomDTO.hotelID;
            room.RoomNumber = roomDTO.roomNumber;
            room.RoomSquare = roomDTO.roomSquare;
            room.IsActive = roomDTO.isActive;
            room.RoomDetailID = roomDTO.iddetail;
            await _unitOfWork.SaveChangesAsync();
            // add bed
          
            var bedRoom =  _unitOfWork.Repository<BedRoom>().GetAllAsync().Result.Where(x=>x.RoomID==idRoom).FirstOrDefault();
            bedRoom.BedID = roomDTO.idBed;
            bedRoom.Quantity = roomDTO.quantity;
           
            await _unitOfWork.SaveChangesAsync();


            //add image
            var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }
            //check folder image exci
            var imgRooms = new List<ImageRooms>();

            foreach (var image in roomDTO.Images)
            {
                if (image.Length > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + image.FileName;

                    var filePath = Path.Combine(imageDirectory, fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await image.CopyToAsync(stream);
                    }
                    var imgRoom = new ImageRooms();
                    imgRoom.NameFileImg = fileName;
                    imgRoom.RoomID = room.RoomID;
                    imgRooms.Add(imgRoom);
                }

            }
            await _unitOfWork.Repository<ImageRooms>().AddListAsync(imgRooms);
            await _unitOfWork.SaveChangesAsync();
            reponse.returnCode = 200;
            reponse.returnMessage = "Thêm Phòng khách sạn thành công";
            return reponse;
        }
    }
}
