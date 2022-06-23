using WebApplication2.Models;

namespace MyApp.ApplicationLogic
{
    public interface IOrdersScreenUseCases
    {
        Task<Order> ViewOrderById(int id);
        Task<IEnumerable<Order>> ViewOrders();
        Task<IEnumerable<Order>> SearchOrders(string id);
        Task delete(int id);
        Task<Order> Create(Order order);
        Task<OrderItem> getItem(int orderId, int indexItem);
        Task<OrderItem> addItem(int orderId, OrderItem item);
        Task updateItem(int orderId, int indexItem, OrderItem item);
        Task deleteItem(int orderId, int indexItem);
    }
}