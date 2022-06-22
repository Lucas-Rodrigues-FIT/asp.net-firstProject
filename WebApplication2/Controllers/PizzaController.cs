using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.DataStore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/pizzas")]
    public class PizzaController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly DataContext db;

        public PizzaController(DataContext db)
        {
            this.db = db;
        }

        //get all pizzas
        [HttpGet]
        public async Task<ActionResult<List<Pizza>>> getAll()
        {
            return Ok(db.pizzas.ToList());
        }

        //get pizza by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> getById(int id)
        {
            if (await db.pizzas.FindAsync(id) == null)
                return NotFound();
            return Ok(await db.pizzas.FindAsync(id));
        }

        //create a new pizza
        [HttpPost]
        public async Task<IActionResult> add(Pizza pizza)
        {
            foreach (var pizza1 in db.pizzas.ToList())
            {
                if (pizza.name == pizza1.name)
                    return BadRequest("This Pizza Name is already exists.");

            }
            pizza.id = db.pizzas.ToList().Max( p => p.id) + 1;
            await db.pizzas.AddAsync(pizza);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(add), new { id = pizza.id }, pizza);
        }

        //delete a pizza by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            if (await db.pizzas.FindAsync(id) == null)
                return NotFound();

            List<Order> orders = await db.orders
                .Include(item => item.orderItems)
                .ThenInclude(pizza => pizza.pizza)
                .ToListAsync();

            foreach (Order order in orders)
                foreach (OrderItem item in order.orderItems)
                    if (item.pizzaId == id)
                        return BadRequest("Exist orders with this pizza.");
            db.pizzas.Remove(db.pizzas.Find(id));
            await db.SaveChangesAsync();
            return NoContent();
        }

        //update 
        [HttpPut("{id}")]
        public async Task<IActionResult> update([FromRoute] int id, [FromBody] Pizza pizza)
        {
            if (db.pizzas.Find(id) == null)
                return NotFound();
            Pizza newPizza = await db.pizzas.FindAsync(id);
            foreach (var pizza1 in db.pizzas.ToList())
            {
                if (pizza1.id != pizza.id)
                    if (pizza.name == pizza1.name)
                        return BadRequest("This Pizza Name is already exists.");

            }
            if (pizza.name != null)
                newPizza.name = pizza.name;
            if (pizza.isGlutenFree != null)
                newPizza.isGlutenFree = pizza.isGlutenFree;
            if (pizza.price != null)
                newPizza.price = pizza.price;

            db.pizzas.Update(newPizza);
            await db.SaveChangesAsync();
            return NoContent();

        }

    }
}
