using WebApplication2.Models;

namespace WebApplication2.Services;

public static class PizzaService
{
    static List<Pizza> pizzas { get; }
    
    static PizzaService()
    {
        pizzas = new List<Pizza>
        {
            new Pizza { id = 1, name = "Cheese 'n Bacon", isGlutenFree = false },
            new Pizza { id = 2, name = "Classic Brazilian", isGlutenFree = false }
        };
    }

    public static List<Pizza> getAll()
    {
        return pizzas;
    }

    public static Pizza? getById(int id)
    {
        return pizzas.FirstOrDefault(x => x.id == id);
    }

    public static void add(Pizza pizza)
    {
        pizza.id = pizzas.Count + 1;
        pizzas.Add(pizza);
    }

    public static int delete(int id)
    {
        var pizza = getById(id);
        if (pizza == null)
            return -1;

        pizzas.Remove(pizza);
        return 0;
    }

    public static int update(int id,Pizza pizza)
    {
        var index = pizzas.FindIndex(x => x.id == id);
        if (index == -1)
            return -1;

        var pizzaNew = new Pizza {id = id, name = pizza.name, isGlutenFree = pizza.isGlutenFree};

        pizzas[index] = pizzaNew;
        return 0;
    }
}