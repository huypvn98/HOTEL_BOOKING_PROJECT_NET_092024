
using BookingHotel.Core.Repository.Interface;

namespace BookingHotel.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}