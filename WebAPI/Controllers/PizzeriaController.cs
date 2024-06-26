using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api.[controller]")]
    public class PizzeriaController : ControllerBase
    {
        [HttpGet("products/{productCategory}")]
        public ActionResult<List<Product>> GetProducts(IDataReader dataReader, string productCategory)
        {
            var products = dataReader.GetProducts(productCategory);

            if (products != null)
            {
                return products;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("products/{productCategory}/{productId}")]
        public ActionResult<Product> GetProduct(IDataReader dataReader, string productCategory, int productId)
        {
            var product = dataReader.GetProduct(productCategory, productId);

            if (product != null)
            {
                return product;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("images/{productCategory}/{productId}")]
        public IActionResult GetProductImage(IDataReader dataReader, string productCategory, string productId)
        {
            string path = dataReader.GetImagePath(productCategory, productId);
            if (Path.Exists(path))
            {
                return PhysicalFile(path, "image/avif");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("cities")]
        public ActionResult<List<string>> GetCities(IDataReader dataReader)
        {
            List<string>? cities = dataReader.GetCities();
            if (cities != null)
            {
                return cities;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{city}/pizzerias")]
        public ActionResult<List<Pizzeria>> GetPizzerias(IDataReader dataReader, string city)
        {
            List<Pizzeria>? pizzerias = dataReader.GetPizzerias(city);
            if (pizzerias != null)
            {
                return pizzerias;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("checkout")]
        public IActionResult PostOrder([FromForm] Data data)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(data.CartProducts);
            if (products != null && products.Count>0)
            {
                Random random = new Random();
                return Content($"Your order has been placed! Your order number is {random.Next(1,1000)}");
            }
            else
            {
                return BadRequest("Error. We are currently unable to process your order!");
            }
        }
    }
}
