using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> Orders = new List<Order>
        {
            new Order { Id = 1, UserId = 1, Item = "Laptop", Price = 1200.00m },
            new Order { Id = 2, UserId = 2, Item = "Smartphone", Price = 800.00m }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return Ok(Orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("byUser/{userId}")]
        public ActionResult<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            var userOrders = Orders.Where(o => o.UserId == userId).ToList();
            if (!userOrders.Any())
            {
                return NotFound();
            }
            return Ok(userOrders);
        }
    }
}
