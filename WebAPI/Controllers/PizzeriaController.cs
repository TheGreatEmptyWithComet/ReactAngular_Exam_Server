using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api.[controller]")]
    public class PizzeriaController : ControllerBase
    {
        [Route("products/{product}")]
        public string GetProduct(IDataReader dataReader, string product)
        {
            string dataAsJson = dataReader.GetJsonData(product);
            return dataAsJson;
        }

        [Route("images/{productCategory}/{productId}")]
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
    }
}
