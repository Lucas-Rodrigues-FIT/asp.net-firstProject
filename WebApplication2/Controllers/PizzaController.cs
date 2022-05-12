using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Services;
using WebApplication2.Filters;

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

        public PizzaController()
        {

        }

        //get all pizzas
        [HttpGet]
        public ActionResult<List<Pizza>> getAll()
        {
            return Ok(PizzaService.getAll());
        }

        //get pizza by id
        [HttpGet("{id}")]
        public ActionResult<Pizza> getById(int id)
        {
            if (PizzaService.getById(id) == null)
                return NotFound();
            return Ok(PizzaService.getById(id));
        }

        //create a new pizza
        [HttpPost]
        [Pizza_EnsureNotExistsDuplicateName]
        public IActionResult add(Pizza pizza)
        {
            PizzaService.add(pizza);
            return CreatedAtAction(nameof(add), new {id = pizza.id}, pizza);
        }

        //delete a pizza by id
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            if(PizzaService.delete(id) == -1)
                return NotFound();
            else
                return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult update([FromRoute] int id, [FromBody] Pizza pizza)
        {
            if (PizzaService.update(id,pizza) == -1)
                return NotFound();
            else
                return NoContent();
        }

    }
}
