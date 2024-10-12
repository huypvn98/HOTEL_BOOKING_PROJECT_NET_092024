using BackendAPIBookingHotel.Model;
using BookingHotel.Core;
using BookingHotel.Core.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
        {
            //private IRoomRepository _roomServices;
            private IHotelGenericRepository _hotelGenericRepository;
            private IUnitOfWork _unitOfWork;
            private readonly ILogger<HotelController> _logger;
            public HotelController(
                ILogger<HotelController> logger,
                IHotelGenericRepository hotelGenericRepository,
                IUnitOfWork unitOfWork)
            {
                _logger = logger;
                _hotelGenericRepository = hotelGenericRepository;
                _unitOfWork = unitOfWork;
            }

            [HttpGet("GetAll")]
            public async Task<ActionResult<IEnumerable<Hotel>>> GetAllHotels([FromQuery] string keyword = "")
            {
                try
                {
                    var hotels = await _hotelGenericRepository.GetAllAsync();

                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        hotels = hotels.Where(h => h.HotelName.Contains(keyword, StringComparison.OrdinalIgnoreCase) || h.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                                            .ToList();
                    }

                    return Ok(hotels);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in GetAllHotels");
                    return StatusCode(500, new { error = "An error occurred while processing your request.", details = ex.Message });
                }
            }

            [HttpPost("Insert")]
            public async Task<ActionResult<int>> InsertHotel(Hotel_InsertRequestData requestData)
            {
                try
                {
                    // Kiểm tra ràng buộc HotelName không được trống
                    if (string.IsNullOrWhiteSpace(requestData.HotelName))
                    {
                        return BadRequest("HotelName cannot be empty.");
                    }

                    // Kiểm tra ràng buộc HotelName không được trùng
                    var existingHotel = await _hotelGenericRepository.GetAsync(h => h.HotelName.Equals(requestData.HotelName, StringComparison.OrdinalIgnoreCase));
                    if (existingHotel != null)
                    {
                        return BadRequest("HotelName already exists.");
                    }

                    var newHotel = new Hotel
                    {
                        CreatedDate = DateTime.UtcNow,
                        HotelName = requestData.HotelName,
                        Description = requestData.Description,
                    };

                    await _unitOfWork.Repository<Hotel>().AddAsync(newHotel);

                    var rs = await _unitOfWork.SaveChangesAsync(); // Sửa đổi phương thức gọi

                    return Ok(rs);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in InsertHotel");
                    return StatusCode(500, new { error = "An error occurred while processing your request.", details = ex.Message });
                }
            }

            [HttpPut("Update/{id}")]
            public async Task<ActionResult> UpdateHotel(int id, Hotel_InsertRequestData requestData)
            {
                try
                {
                    // Kiểm tra ràng buộc HotelName không được trống
                    if (string.IsNullOrWhiteSpace(requestData.HotelName))
                    {
                        return BadRequest("HotelName cannot be empty.");
                    }

                    var existingHotel = await _hotelGenericRepository.GetByIdAsync(id); // Sửa đổi phương thức gọi
                    if (existingHotel == null)
                    {
                        return NotFound($"Hotel with ID {id} not found.");
                    }

                    // Kiểm tra ràng buộc HotelName không được trùng
                    var duplicateHotel = await _hotelGenericRepository.GetAsync(h => h.HotelName.Equals(requestData.HotelName, StringComparison.OrdinalIgnoreCase) && h.HotelID != id);
                    if (duplicateHotel != null)
                    {
                        return BadRequest("HotelName already exists.");
                    }

                    existingHotel.HotelName = requestData.HotelName;
                    existingHotel.Description = requestData.Description;
                    // Update other properties as needed

                    await _unitOfWork.Repository<Hotel>().UpdateAsync(existingHotel); // Sửa đổi phương thức gọi
                    var affectedRows = await _unitOfWork.SaveChangesAsync(); // Sửa đổi phương thức gọi

                    return Ok(affectedRows);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error in UpdateHotel for ID {id}");
                    return StatusCode(500, new { error = "An error occurred while processing your request.", details = ex.Message });
                }
            }

            [HttpDelete("Delete/{id}")]
            public async Task<ActionResult> DeleteHotel(int id)
            {
                try
                {
                    var existingHotel = await _hotelGenericRepository.GetByIdAsync(id); // Sửa đổi phương thức gọi
                    if (existingHotel == null)
                    {
                        return NotFound($"Hotel with ID {id} not found.");
                    }

                    await _unitOfWork.Repository<Hotel>().DeleteAsync(id); // Sửa đổi phương thức gọi
                    var affectedRows = _unitOfWork.SaveChangesAsync();

                    return Ok(affectedRows);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error in DeleteHotel for ID {id}");
                    return StatusCode(500, new { error = "An error occurred while processing your request.", details = ex.Message });
                }
            }
        }
    }
