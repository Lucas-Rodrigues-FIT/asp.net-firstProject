using WebApplication2.Models;

namespace Repositories.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> get();
        Task<Order> getById(int id);
        Task delete(int id);
        Task<Order> Create(Order order);
        Task<OrderItem> getItem(int orderId, int indexItem);
        Task<OrderItem> addItem(int orderId, OrderItem item);
        Task updateItem(int orderId, int indexItem, OrderItem item);
        Task deleteItem(int orderId, int indexItem);
    }
}