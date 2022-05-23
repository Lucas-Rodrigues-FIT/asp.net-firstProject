using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.DataStore;

namespace WebApplication2.Controllers
{

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
        public ActionResult<List<Pizza>> getAll()
        {
            return Ok(db.pizzas.ToList());
        }

        //get pizza by id
        [HttpGet("{id}")]
        public ActionResult<Pizza> getById(int id)
        {
            if (db.pizzas.Find(id) == null)
                return NotFound();
            return Ok(db.pizzas.Find(id));
        }

        //create a new pizza
        [HttpPost]
        public IActionResult add(Pizza pizza)
        {
            foreach(var pizza1 in db.pizzas.ToList())
            {
                if(pizza.name == pizza1.name)
                {
                    return BadRequest("This Pizza Name is already exits");
                }
            }
            pizza.id = db.pizzas.ToList().Count + 1;
            db.pizzas.Add(pizza);
            db.SaveChanges();
            return CreatedAtAction(nameof(add), new {id = pizza.id}, pizza);
        }

        //delete a pizza by id
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            if(db.pizzas.Find(id) == null)
                return NotFound();
            db.pizzas.Remove(db.pizzas.Find(id));
            db.SaveChanges();
            return NoContent();
        }

        //update 
        [HttpPut("{id}")]
        public IActionResult update([FromRoute] int id, [FromBody] Pizza pizza)
        {
            if (db.pizzas.Find(id) == null)
                return NotFound();
            Pizza newPizza = db.pizzas.Find(id);
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
            db.SaveChanges();
            return NoContent();
            
        }

    }
}
