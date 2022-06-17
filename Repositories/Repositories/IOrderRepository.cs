using WebApplication2.Models;

namespace Repositories.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> get();
        Task<Order> getById(int id);
    }
}