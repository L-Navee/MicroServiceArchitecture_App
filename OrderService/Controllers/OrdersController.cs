using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly UserServiceClient _userServiceClient;

        public OrdersController(UserServiceClient userServiceClient)
        {
            _userServiceClient = userServiceClient;
        }

        private static readonly List<Order> Orders = new List<Order>
        {
            new Order { Id = 1, UserId = 1, Item = "Laptop", Price = 1200.00m },
            new Order { Id = 2, UserId = 2, Item = "Smartphone", Price = 800.00m }
        };

        [HttpGet("withUser/{id}")]
        public async Task<IActionResult> GetOrderWithUser(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            var user = await _userServiceClient.GetUserById(order.UserId);
            return Ok(new { order, user });
        }
    }
}
