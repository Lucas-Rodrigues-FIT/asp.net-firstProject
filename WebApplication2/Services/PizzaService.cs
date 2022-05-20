using WebApplication2.DataStore;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Services;

public class PizzaService
{
    private readonly DataContext db;

    public PizzaService(DataContext _dataContext)
    {
        this.db = db;
    }

    public PizzaService()
    {
    }

    public List<Pizza> getAll()
    {
        return db.pizzas.ToList();
    }

    public Pizza? getById(int id)
    {
        return db.pizzas.Find(id);
    }

    public void add(Pizza pizza)
    {
        pizza.id = db.pizzas.ToList().Count + 1;
        db.pizzas.Add(pizza);
    }

    public int delete(int id)
    {
        var pizza = db.pizzas.Find(id);
        if (pizza == null)
            return -1;

        db.pizzas.Remove(pizza);
        return 0;
    }

    public int update(int id,Pizza pizza)
    {
        var index = db.pizzas.Find(id);
        if (index == null)
            return -1;

        var pizzaNew = new Pizza {id = id, name = pizza.name, isGlutenFree = pizza.isGlutenFree};

        db.pizzas.Update(pizzaNew);
        return 0;
    }
}