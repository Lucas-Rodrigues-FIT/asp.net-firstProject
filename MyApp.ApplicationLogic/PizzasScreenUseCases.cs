using WebApplication2.Models;
using WebApplication2.Repositories;

namespace MyApp.ApplicationLogic
{
    public class PizzasScreenUseCases : IPizzasScreenUseCases
    {
        private readonly IPizzaRepository pizzaRepository;

        public PizzasScreenUseCases(IPizzaRepository pizzaRepository)
        {
            this.pizzaRepository = pizzaRepository;
        }

        public async Task<IEnumerable<Pizza>> ViewPizzas()
        {
            return await pizzaRepository.get();
        }

        public async Task<Pizza> ViewPizzaById(int id)
        {
            return await pizzaRepository.getById(id);
        }

        public async Task<IEnumerable<Pizza>> SearchPizzas(String filter)
        {
            return await pizzaRepository.get(filter);
        }

        public async Task<int> addPizza(Pizza pizza)
        {
            return await pizzaRepository.Create(pizza);
        }

        public async Task updatePizza(Pizza pizza)
        {
            await pizzaRepository.Update(pizza);
        }

        public async Task deletePizza(int id)
        {
            await pizzaRepository.Delete(id);
        }
    }
}