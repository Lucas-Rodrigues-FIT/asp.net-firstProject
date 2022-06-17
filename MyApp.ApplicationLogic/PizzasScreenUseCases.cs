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

        public async Task<IEnumerable<Pizza>> SearchPizzas(String filter)
        {
            return await pizzaRepository.search(filter);
        }
    }
}