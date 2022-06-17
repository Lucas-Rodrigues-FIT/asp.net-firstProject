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
    }
}
