using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.DataStore;
using Microsoft.EntityFrameworkCore;

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

        //get all order
        [HttpGet]
        public async Task<ActionResult<List<Order>>> getAll()
        {
            return Ok(await db.orders
                .Include(item => item.orderItems)
                .ThenInclude(pizza => pizza.pizza)
                .ToListAsync());
        }

        //get all orderItems
        [HttpGet("items")]
        public async Task<ActionResult<List<Order>>> getAllItems()
        {
            return Ok(await db.orderItems.ToListAsync());
        }

        //get order by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> getById(int id)
        {
            if (await db.orders.FindAsync(id) == null)
                return NotFound();
            return Ok(db.orders.Include(item => item.orderItems)
                .ThenInclude(pizza => pizza.pizza).ToList()
                .Find(order => order.id == id));
        }

        //create a new order
        [HttpPost]
        public async Task<IActionResult> add(Order order)
        {
            foreach (var item in order.orderItems)
            {
                if (await db.pizzas.FindAsync(item.pizzaId) == null)
                    return BadRequest("This pizza or these pizzas not exist.");
            }

            order.orderItems = order.orderItems.Where(item => item.quantity > 0).ToList();

            order.id = db.orders.ToList().Last().id + 1;

            foreach (var item in order.orderItems)
                item.orderId = order.id;

            await db.orders.AddAsync(order);
            await db.SaveChangesAsync();

            foreach (var item in order.orderItems)
            {
                item.pizza = await db.pizzas.FindAsync(item.pizzaId);
            }

            return CreatedAtAction(nameof(add), new { id = order.id }, order);
        }

        //delete a order by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            if (db.orders.Find(id) == null)
                return NotFound();

            foreach (var item in db.orders.Include(item => item.orderItems)
                .ThenInclude(pizza => pizza.pizza).ToList()
                .Find(order => order.id == id).orderItems)
            {
                item.orderId = null;
                db.pizzas.Find(item.pizzaId).orderItems.Remove(item);
                item.pizzaId = null;
                db.orderItems.Remove(db.orderItems.Find(item.Id));
            }
            await db.SaveChangesAsync();

            db.orders.Remove(await db.orders.FindAsync(id));
            await db.SaveChangesAsync();
            return NoContent();
        }

        //update 
        [HttpPut("{id}")]
        public async Task<IActionResult> update([FromRoute] int id, [FromBody] Order order)
        {
            if (await db.orders.FindAsync(id) == null)
                return NotFound();
            Order newOrder = db.orders.Include(item => item.orderItems)
                .ThenInclude(pizza => pizza.pizza).ToList()
                .Find(order => order.id == id);

            foreach (var item in order.orderItems)
            {
                if (db.pizzas.Find(item.pizzaId) == null)
                    return BadRequest("This pizza or these pizzas not exist.");
            }

            foreach (var item in newOrder.orderItems)
            {
                item.orderId = null;
                db.pizzas.Find(item.pizzaId).orderItems.Remove(item);
                item.pizzaId = null;
                db.orderItems.Remove(db.orderItems.Find(item.Id));
            }
            await db.SaveChangesAsync();

            newOrder.orderItems = order.orderItems.Where(item => item.quantity > 0).ToList();

            db.orders.Update(newOrder);
            await db.SaveChangesAsync();
            return NoContent();
        }

        //get items for order
        [HttpGet("{id}/items/{indexItem}")]
        public async Task<ActionResult<OrderItem>> getItem([FromRoute] int id, [FromRoute] int indexItem)
        {
            Order order = db.orders.Include(item => item.orderItems)
                .ThenInclude(pizza => pizza.pizza).ToList()
                .Find(order => order.id == id);
            if (order == null)
                return NotFound();
            if (order.orderItems.Count < indexItem)
                return NotFound();

            return Ok(order.orderItems.ElementAt(indexItem-1));
        }

        // add items in order
        [HttpPost("{id}/items")]
        public async Task<IActionResult> addItem([FromRoute] int id, [FromBody] OrderItem item)
        {
            Order order = db.orders.Include(item => item.orderItems)
                .ThenInclude(pizza => pizza.pizza).ToList()
                .Find(order => order.id == id);
            if (order == null)
                return NotFound();

            foreach (OrderItem itemAux in order.orderItems)
            {
                if (itemAux.pizzaId == item.pizzaId)
                    return BadRequest($"This pizza is already in the order.");
            }
            if (!item.validateQuantity())
                return BadRequest("Add a valited Quantity.");
            if (!item.validatePizzaId())
                return BadRequest("Add a valited Pizza ID.");

            order.orderItems.Add(item);
            db.Update(order);
            await db.SaveChangesAsync();

            item.Id = db.orderItems.ToList().Last().Id;

            return CreatedAtAction(nameof(add), new { id = item.Id }, item);
        }

        // delete a item for order
        [HttpDelete("{id}/items/{indexItem}")]
        public async Task<IActionResult> deleteItem([FromRoute] int id, [FromRoute] int indexItem)
        {
            Order order = db.orders.Include(item => item.orderItems)
                .ThenInclude(pizza => pizza.pizza).ToList()
                .Find(order => order.id == id);
            if (order == null)
                return NotFound();
            if (order.orderItems.Count < indexItem)
                return NotFound();

            db.orderItems.Remove(order.orderItems.ToList().ElementAt(indexItem - 1));
            order.orderItems.RemoveAt(indexItem - 1);
            db.orders.Update(order);
            await db.SaveChangesAsync();
            return NoContent();
        }

        // update a item for order
        [HttpPut("{id}/items/{indexItem}")]
        public async Task<IActionResult> updateItem([FromRoute] int id, [FromRoute] int indexItem, [FromBody] OrderItem item)
        {
            Order order = db.orders.Include(item => item.orderItems)
                .ThenInclude(pizza => pizza.pizza).ToList()
                .Find(order => order.id == id);
            if (order == null)
                return NotFound();
            if (order.orderItems.Count < indexItem)
                return NotFound();
            if (!item.validateQuantity())
                return BadRequest("Add a valited Quantity.");

            order.orderItems.ElementAt(indexItem - 1).quantity = item.quantity;
            db.orders.Update(order);
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}

