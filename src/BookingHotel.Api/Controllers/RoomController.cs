﻿using Azure;
using BackendAPIBookingHotel.Model;
using BookingHotel.Core;
using BookingHotel.Core.DTO;
using BookingHotel.Core.Models;
using BookingHotel.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingHotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IUnitOfWork _unitOfWork;

        public RoomController(IRoomService roomService, IUnitOfWork unitOfWork)
        {
            _roomService=roomService;
            _unitOfWork= unitOfWork;
        }



        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var reponse= new RetureReponse();
            try
            {
                var room = await _roomService.getRoomById(id);
                if(room == null)
                {
                    reponse.returnCode = 404;
                    reponse.returnMessage = "Không tìm thấy phòng";
                    return NotFound(reponse);
                }
                var RoomDTO = new RoomDTO()
                {
                    hotelID = room.HotelID,
                    roomNumber = room.RoomNumber,
                    roomSquare = room.RoomSquare,
                    isActive = room.IsActive,
                };
                return Ok(RoomDTO);
            
            }catch (Exception ex)
            {
                reponse.returnCode = 500;
                reponse.returnMessage = "Lỗi dữ liệu "+ex.Message;
                return BadRequest(reponse);
            }
          
        }
        // GET api/<RoomController>/5
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var reponse = new RetureReponse();
            try
            {
                var room = await _roomService.getAll();
                
               if (room.Count>0)
                {
                    var roomDTO = room.Select(x => new RoomDTO()
                    {
                        hotelID = x.HotelID,
                        roomNumber = x.RoomNumber,
                        roomSquare = x.RoomSquare,
                        isActive = x.IsActive
                    }) ;
                    return Ok(roomDTO);
                }
                reponse.returnCode = 404;
                reponse.returnMessage = "Không dữ liệu nào được tìm thấy";
                return NotFound(reponse);

            }
            catch (Exception ex)
            {
                reponse.returnCode = 500;
                reponse.returnMessage = "Lỗi dữ liệu " + ex.Message;
                return BadRequest(reponse);
            }

        }
        // POST api/<RoomController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] RoomDTO roomRequest)
        {

            var returnRespone = new RetureReponse();

            try
            {
                var hotel = _unitOfWork.Repository<Hotel>().GetByIdAsync(roomRequest.hotelID).Result;
                if (hotel == null)
                {
                    returnRespone.returnCode = 404;
                    returnRespone.returnMessage = "Khách sạn không tìm thấy";
                    return NotFound(returnRespone);
                }
             
                if (roomRequest.Images.Count == 0)
                {
                    returnRespone.returnCode = 400;
                    returnRespone.returnMessage = "Vui lòng chọn ảnh";
                    return BadRequest(returnRespone);

                }

                var bed = _unitOfWork.Repository<Bed>().GetByIdAsync(roomRequest.idBed).Result;
                if (bed == null)
                {
                    returnRespone.returnCode = 404;
                    returnRespone.returnMessage = "Loại giường không tìm th";
                    return NotFound(returnRespone);
                }
                returnRespone = _roomService.InsertRoom(roomRequest).Result;
                return Ok(returnRespone);
            }catch(Exception ex)
            {
                return BadRequest("Lỗi khi thêm dữ liệu "+ex.Message );
            }
           
        }

        // POST api/<RoomController>
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(int id,[FromBody] RoomDTO roomRequest )
        {
            var returnRespone = new RetureReponse();

            try
            {
                var room = await _roomService.getRoomById(id);
                if (room == null)
                {
                    returnRespone.returnCode = 404;
                    returnRespone.returnMessage = "Không tìm thấy phòng";
                    return NotFound(returnRespone);
                }
                var hotel = _unitOfWork.Repository<Hotel>().GetByIdAsync(roomRequest.hotelID).Result;
                if (hotel == null)
                {
                    returnRespone.returnCode = 404;
                    returnRespone.returnMessage = "Khách sạn không tìm thấy";
                    return BadRequest(returnRespone);
                }
                returnRespone = _roomService.UpdateRoom(id,roomRequest).Result;
                return Ok(returnRespone);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi cập nhật dữ liệu " + ex.Message);
            }

        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var returnRespone = new RetureReponse();

            try
            {
                var room = await _roomService.getRoomById(id);
                if (room == null)
                {
                    returnRespone.returnCode = 404;
                    returnRespone.returnMessage = "Không tìm thấy phòng";
                    return NotFound(returnRespone);
                }
                await  _roomService.DeleteRoom(id);
                returnRespone.returnCode = 200;
                returnRespone.returnMessage = "Xóa phòng thành công";
                return Ok(returnRespone);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi xóa dữ liệu " + ex.Message);
            }
        }
        }
}
