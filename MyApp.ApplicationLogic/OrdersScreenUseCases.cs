using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Repositories;
using WebApplication2.Models;

namespace MyApp.ApplicationLogic
{
    public class OrdersScreenUseCases : IOrdersScreenUseCases
    {
        private readonly IOrderRepository orderRepository;

        public OrdersScreenUseCases(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> ViewOrders()
        {
            return await orderRepository.get();
        }

        public async Task<Order> ViewOrderById(int id)
        {
            return (Order)await orderRepository.getById(id);
        }

        public async Task<IEnumerable<Order>> SearchOrders(string id)
        {
            if (int.TryParse(id, out int orderId))
            {
                var order = await orderRepository.getById(orderId);
                var orders = new List<Order>();
                orders.Add(order);
                return orders;
            }

            return await orderRepository.get();
        }

        public async Task delete(int id)
        {
            await orderRepository.delete(id);
        }

        public async Task<Order> Create(Order order)
        {
            return await orderRepository.Create(order);
        }

        public async Task<OrderItem> getItem(int orderId, int indexItem)
        {
            return await orderRepository.getItem(orderId, indexItem);
        }

        public async Task<OrderItem> addItem(int orderId, OrderItem item)
        {
            return await orderRepository.addItem(orderId,item);
        }

        public async Task updateItem(int orderId, int indexItem, OrderItem item)
        {
            await orderRepository.updateItem(orderId, indexItem, item);
        }

        public async Task deleteItem(int orderId, int indexItem)
        {
            await orderRepository.deleteItem(orderId, indexItem);
        }
    }
}
