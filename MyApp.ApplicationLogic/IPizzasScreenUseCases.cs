using WebApplication2.Models;

namespace MyApp.ApplicationLogic
{
    public interface IPizzasScreenUseCases
    {
        Task<IEnumerable<Pizza>> ViewPizzas();
    }
}