using Microsoft.AspNetCore.Mvc;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ProductApiService _productApi;

        public OrderController(ProductApiService productApi)
        {
            _productApi = productApi;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> PlaceOrder(int productId)
        {
            // Call ProductService to get product details
            var product = await _productApi.GetProductByIdAsync(productId);

            if (product == null)
                return NotFound("Product not found in ProductService");

            var order = new Order
            {
                OrderId = Guid.NewGuid().ToString(),
                ProductId = product.Id,
                ProductName = product.Name,
                TotalPrice = product.Price,
                OrderDate = DateTime.Now
            };

            return Ok(order);
        }
    }

    public class Order
    {
        public string OrderId { get; set; } = "";
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
