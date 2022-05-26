using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.DataStore;
using WebApplication2.Filters.v2;

namespace WebApplication2.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/pizzas")]
    public class PizzaControllerV2 : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly DataContext db;

        public PizzaControllerV2(DataContext db)
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
        [Pizza_EnsurePizzaNotBeNamedPizza]
        public async Task<IActionResult> add(Pizza pizza)
        {
            foreach(var pizza1 in db.pizzas.ToList())
            {
                if(pizza.name == pizza1.name)
                {
                    return BadRequest("This Pizza Name is already exits");
                }
            }
            pizza.id = db.pizzas.ToList().Count + 1;
            await db.pizzas.AddAsync(pizza);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(add), new {id = pizza.id}, pizza);
        }

        //delete a pizza by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            if(await db.pizzas.FindAsync(id) == null)
                return NotFound();
            db.pizzas.Remove(db.pizzas.Find(id));
            await db.SaveChangesAsync();
            return NoContent();
        }

        //update 
        [HttpPut("{id}")]
        [Pizza_EnsurePizzaNotBeNamedPizza]
        public async Task<IActionResult> update([FromRoute] int id, [FromBody] Pizza pizza)
        {
            if (db.pizzas.Find(id) == null)
                return NotFound();
            Pizza newPizza = await db.pizzas.FindAsync(id);
            foreach (var pizza1 in db.pizzas.ToList())
            {
                if (pizza.name == pizza1.name)
                {
                    return BadRequest("This Pizza Name is already exits");
                }
            }
            if (pizza.name != null)
                newPizza.name = pizza.name;
            if (pizza.isGlutenFree != null)
                newPizza.isGlutenFree = pizza.isGlutenFree;
            if(pizza.price != null)
                newPizza.price = pizza.price;

            db.pizzas.Update(newPizza);
            await db.SaveChangesAsync();
            return NoContent();
            
        }

    }
}
