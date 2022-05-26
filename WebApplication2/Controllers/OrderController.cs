using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.DataStore;

namespace WebApplication2.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly DataContext db;

        public OrderController(DataContext db)
        {
            this.db = db;
        }

        //get all pizzas
        [HttpGet]
        public ActionResult<List<Order>> getAll()
        {
            return Ok(db.orders.ToList());
        }

        //get pizza by id
        [HttpGet("{id}")]
        public ActionResult<Order> getById(int id)
        {
            if (db.orders.Find(id) == null)
                return NotFound();
            return Ok(db.orders.Find(id));
        }

        //create a new pizza
        [HttpPost]
        public IActionResult add(Order order)
        {
            order.id = db.orders.ToList().Count + 1;
            db.orders.Add(order);
            db.SaveChanges();
            return CreatedAtAction(nameof(add), new {id = order.id}, order);
        }

        //delete a pizza by id
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            if(db.orders.Find(id) == null)
                return NotFound();
            db.orders.Remove(db.orders.Find(id));
            db.SaveChanges();
            return NoContent();
        }

        //update 
        [HttpPut("{id}")]
        public IActionResult update([FromRoute] int id, [FromBody] Order order)
        {
            if (db.orders.Find(id) == null)
                return NotFound();
            
            order.id = id;

            db.orders.Update(order);
            db.SaveChanges();
            return NoContent();
            
        }

    }
}
