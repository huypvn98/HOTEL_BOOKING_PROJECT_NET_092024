using System.Linq.Expressions;
using BackendAPIBookingHotel.Model;
using BookingHotel.Core.Persistence;
using BookingHotel.Core.Repository.Interface;
using Microsoft.EntityFrameworkCore;

public class HotelGenericRepository : IHotelGenericRepository
{
    private readonly HotelBookingDbContext _context;

    public HotelGenericRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Hotel entity)
    {
        await _context.Hotels.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var hotel = await _context.Hotels.FindAsync(id);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync()
    {
        return await _context.Hotels.ToListAsync();
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync(Expression<Func<Hotel, bool>> predicate)
    {
        return await _context.Hotels.Where(predicate).ToListAsync();
    }

    public async Task<Hotel> GetAsync(Expression<Func<Hotel, bool>> predicate)
    {
        return await _context.Hotels.FirstOrDefaultAsync(predicate);
    }

    public async Task<Hotel> GetByIdAsync(int id)
    {
        return await _context.Hotels.FindAsync(id);
    }

    public async Task UpdateAsync(Hotel entity)
    {
        _context.Hotels.Update(entity);
        await _context.SaveChangesAsync();
    }
}