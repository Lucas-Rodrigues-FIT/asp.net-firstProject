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
    }
}
