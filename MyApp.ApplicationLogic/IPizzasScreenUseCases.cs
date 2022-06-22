using WebApplication2.Models;

namespace MyApp.ApplicationLogic
{
    public interface IPizzasScreenUseCases
    {
        Task<IEnumerable<Pizza>> ViewPizzas();
        Task<Pizza> ViewPizzaById(int id);
        Task<IEnumerable<Pizza>> SearchPizzas(String filter);
        Task<int> addPizza(Pizza pizza);
        Task updatePizza(Pizza pizza);
        Task deletePizza(int id);
    }
}