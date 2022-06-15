using WebApplication2.ApiClient;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        public PizzaRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Pizza>> get()
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Pizza>>("api/pizzas");
        }

        public async Task<IEnumerable<Pizza>> getById(int id)
        {
            return await webApiExecuter.InvokeGet<IEnumerable<Pizza>>($"api/pizzas/{id}");
        }

        public async Task<int> Create(Pizza pizza)
        {
            pizza = await webApiExecuter.InvokePost("api/pizzas", pizza);
            return pizza.id;
        }

        public async Task Update(Pizza pizza)
        {
            await webApiExecuter.InvokePut($"api/pizzas/{pizza.id}", pizza);
        }

        public async Task Delete(int id)
        {
            await webApiExecuter.InvokeDelete($"api/pizzas/{id}");
        }
    }
}
