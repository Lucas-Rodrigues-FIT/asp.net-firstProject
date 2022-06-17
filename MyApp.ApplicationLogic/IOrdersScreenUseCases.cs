using WebApplication2.Models;

namespace MyApp.ApplicationLogic
{
    public interface IOrdersScreenUseCases
    {
        Task<Order> ViewOrderById(int id);
        Task<IEnumerable<Order>> ViewOrders();
    }
}