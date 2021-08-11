using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevOpsAssignment.Controllers
{
    [ApiController]
    [Route("")]
    public class ProductController : ControllerBase
    {
        private static readonly Product[] Products = new Product[]
        {
            new Product
            {
                ProductId = 1,
                Name = "Shoes 1",
                Price = 1000,
                Description = "Shoes for women",
                ManufacturedOn = DateTime.Now.Date
            },
            new Product
            {
                ProductId = 1,
                Name = "Shoes 2",
                Price = 1000,
                Description = "Shoes for men",
                ManufacturedOn = DateTime.Now.Date
            }
        };

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetMessage()
        {
            _logger.LogInformation("GetMessage called");
            return "Welcome message!";
        }

        [Route("products")]
        [HttpGet]
        public IEnumerable<Product> GetListOfProduct()
        {
            _logger.LogInformation("GetWeatherForecastList method called");
            return Products;
        }
    }
}
