using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IDataReader
    {
        public List<Product> GetProducts(string productCategory);

        public Product GetProduct(string productCategory, int productId);

        public string GetImagePath(string productCategory, string productId);

        public List<string>? GetCities();
        public List<Pizzeria>? GetPizzerias(string city);
    }
}
