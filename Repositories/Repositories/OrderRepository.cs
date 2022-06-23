using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.ApiClient;
using WebApplication2.Models;

namespace Repositories.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        public OrderRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Order>> get()
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Order>>("api/orders");
        }

        public async Task<Order> getById(int id)
        {
            return await webApiExecuter.InvokeGet<Order>($"api/orders/{id}");
        }

        public async Task delete(int id)
        {
            await webApiExecuter.InvokeDelete($"api/orders/{id}");
        }

        public async Task<Order> Create(Order order)
        {
            return await webApiExecuter.InvokePost<Order>($"api/orders", order);
        }

        public async Task<OrderItem> getItem(int orderId, int indexItem)
        {
            return await webApiExecuter.InvokeGet<OrderItem>(
                $"api/orders/{orderId}/items/{indexItem}");
        }

        public async Task<OrderItem> addItem(int orderId, OrderItem item)
        {
            return await webApiExecuter.InvokePost<OrderItem>(
                $"api/orders/{orderId}/items",
                item);
        }

        public async Task updateItem(int orderId, int indexItem, OrderItem item)
        {
            await webApiExecuter.InvokePut<OrderItem>(
                $"api/orders/{orderId}/items/{indexItem}",
                item);
        }

        public async Task deleteItem(int orderId, int indexItem)
        {
            await webApiExecuter.InvokeDelete($"api/orders/{orderId}/items/{indexItem}");
        }
    }
}
