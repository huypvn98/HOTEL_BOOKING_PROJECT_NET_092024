using BackendAPIBookingHotel.Model;
using BookingHotel.Core;
using BookingHotel.Core.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace BookingBooking.Api.Services
{
    public class BookingService
  {
    private readonly IBookingGenericRepository _bookingGenericRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BookingService> _logger;

    public BookingService(
        IBookingGenericRepository bookingGenericRepository,
        IUnitOfWork unitOfWork,
        ILogger<BookingService> logger)
    {
      _bookingGenericRepository = bookingGenericRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }

    public async Task<ResponseData<IEnumerable<Booking>>> GetAllBookingsAsync(string keyword)
    {
      var bookings = await _bookingGenericRepository.GetAllAsync(
          //h => h.isActive == true
          );

      //if (!string.IsNullOrWhiteSpace(keyword))
      //{
      //  bookings = bookings
      //      .Where(h => h.BookingName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
      //                  h.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
      //      .ToList();
      //}

      return new ResponseData<IEnumerable<Booking>>(200, bookings, "Success");
    }

    public async Task<ResponseData<Booking>> GetBookingByIdAsync(int id)
{
    // Tìm booking với ID được cung cấp
    var existingBooking = await _bookingGenericRepository.GetByIdAsync(id);

    if (existingBooking == null)
        return new ResponseData<Booking>(404, null, $"Booking with ID {id} not found.");

    // Trả về booking tìm thấy
    return new ResponseData<Booking>(200, existingBooking, "Success");
}
    public async Task<ResponseData<Booking>> InsertBookingAsync(Booking_InsertRequestData requestData)
    {

      var newBooking = new Booking
      {
        CreatedDate = DateTime.UtcNow.AddHours(7),
        RoomID = requestData.RoomID,
        CustomerID = requestData.CustomerID,
        DepositID = requestData.DepositID,
        FromDate = requestData.FromDate,
        CheckInDate = requestData.CheckInDate,
        CheckOutDate = requestData.CheckOutDate,
        BookingStatus = requestData.BookingStatus,
        ToDate = requestData.ToDate // Khách sạn mới luôn active
      };

      await _unitOfWork.Repository<Booking>().AddAsync(newBooking);
      await _unitOfWork.SaveChangesAsync();

      return new ResponseData<Booking>(201, newBooking, "Booking added successfully.");
    }
    public async Task<ResponseData<string>> UpdateBookingAsync(int id, Booking_InsertRequestData requestData)
    {

      // Tìm booking hiện có theo ID
      var existingBooking = await _bookingGenericRepository.GetByIdAsync(id);

      if (existingBooking == null)
        return new ResponseData<string>(404, null, $"Booking with ID {id} not found.");

            existingBooking.ToDate = requestData.ToDate;
            existingBooking.RoomID = requestData.RoomID;
            existingBooking.DepositID = requestData.DepositID;
            existingBooking.FromDate = requestData.FromDate;
            existingBooking.CheckInDate = requestData.CheckInDate;
            existingBooking.CheckOutDate = requestData.CheckOutDate;
            existingBooking.BookingStatus = requestData.BookingStatus;

      // Lưu thay đổi vào database
      await _unitOfWork.Repository<Booking>().UpdateAsync(existingBooking);
      await _unitOfWork.SaveChangesAsync();

      return new ResponseData<string>(200, null, "Booking updated successfully.");
    }


    public async Task<ResponseData<string>> DeleteBookingAsync(int id)
    {
      var existingBooking = await _bookingGenericRepository.GetByIdAsync(id);

      if (existingBooking == null || existingBooking.isActive == false)
        return new ResponseData<string>(404, null, $"Booking with ID {id} not found or already inactive.");

      existingBooking.isActive = false; // Thực hiện soft delete

      await _unitOfWork.Repository<Booking>().UpdateAsync(existingBooking);
      await _unitOfWork.SaveChangesAsync();

      return new ResponseData<string>(200, null, "Booking deleted successfully!!!.");
    }

    public async Task<ResponseData<string>> UnIsActiveBookingAsync(int id)
    {
      // Tìm khách sạn với ID được cung cấp
      var existingBooking = await _bookingGenericRepository.GetByIdAsync(id);

      if (existingBooking == null)
        return new ResponseData<string>(404, null, $"Booking with ID {id} not found.");

      // Kiểm tra nếu khách sạn đã active
      if (existingBooking.isActive == true)
        return new ResponseData<string>(400, null, "Booking is already active.");

      // Cập nhật trạng thái isActive thành true (khôi phục khách sạn)
      existingBooking.isActive = true;
      existingBooking.UpdatedDate = DateTime.UtcNow.AddHours(7);

      // Lưu thay đổi vào database
      await _unitOfWork.Repository<Booking>().UpdateAsync(existingBooking);
      await _unitOfWork.SaveChangesAsync();

      return new ResponseData<string>(200, null, "Booking is now active.");
    }
  }
}
