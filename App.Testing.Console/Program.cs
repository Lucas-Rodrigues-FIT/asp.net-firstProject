
using WebApplication2.ApiClient;
using WebApplication2.Models;
using WebApplication2.Repositories;

HttpClient httpClient = new HttpClient();
IWebApiExecuter apiExecuter = new WebApiExecuter("http://localhost:8080", httpClient);

await GetPizzas();
await CreateAPizza();
await GetPizzas();
await UpdatePizza();
await GetPizzas();
await DeleteAPizza();
await GetPizzas();

async Task GetPizzas()
{
    PizzaRepository repository = new(apiExecuter);

    Console.WriteLine("------------------------------------");
    Console.WriteLine("Get all of pizzas");
    var pizzas = await repository.get();
    foreach (var pizza in pizzas)
    {
        Console.WriteLine($"ID = {pizza.id}, Name = {pizza.name}");
    }
}

async Task<int> CreateAPizza()
{
    PizzaRepository repository = new(apiExecuter);

    Console.WriteLine("------------------------------------");
    Console.WriteLine("Create a pizza");
    var pizza = new Pizza { name = "À moda da casa", isGlutenFree = false, price = 33.00 };
    return await repository.Create(pizza);
}

async Task UpdatePizza()
{
    PizzaRepository repository = new(apiExecuter);

    Console.WriteLine("------------------------------------");
    Console.WriteLine("Updating pizza");
    var pizza = new Pizza { id = 4, name = "4 queijos", isGlutenFree = false, price = 33.00 };
    await repository.Update(pizza);
}

async Task DeleteAPizza()
{
    PizzaRepository repository = new(apiExecuter);

    Console.WriteLine("------------------------------------");
    Console.WriteLine("Deleting pizza");
    await repository.Delete(4);
}

Console.ReadLine();