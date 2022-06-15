using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface IPizzaRepository
    {
        Task<int> Create(Pizza pizza);
        Task Delete(int id);
        Task<IEnumerable<Pizza>> get();
        Task<IEnumerable<Pizza>> getById(int id);
        Task Update(Pizza pizza);
    }
}