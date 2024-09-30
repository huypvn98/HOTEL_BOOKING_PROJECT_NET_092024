using Azure;
using BackendAPIBookingHotel.Model;
using BookingHotel.Core;
using BookingHotel.Core.DTO;
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

        // GET: api/<RoomController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoomController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoomDTO roomRequest)
        {
            var returnRespone = new RetureReponse();

            try
            {
                var hotel = _unitOfWork.Repository<Hotel>().GetByIdAsync(roomRequest.hotelID).Result;
                if (hotel == null)
                {
                    returnRespone.returnCode = 404;
                    returnRespone.returnMessage = "Khách sạn không tìm thấy";
                    return BadRequest(returnRespone);
                }
                returnRespone = _roomService.InsertRoom(roomRequest).Result;
                return Ok(returnRespone);
            }catch(Exception ex)
            {
                return BadRequest("Lỗi khi thêm dữ liệu "+ex.Message );
            }
           
        }

    }
}
