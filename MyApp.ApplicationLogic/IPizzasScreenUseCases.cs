using WebApplication2.Models;

namespace MyApp.ApplicationLogic
{
    public interface IPizzasScreenUseCases
    {
        Task<IEnumerable<Pizza>> ViewPizzas();
        Task<IEnumerable<Pizza>> SearchPizzas(String filter);
    }
}