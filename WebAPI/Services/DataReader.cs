using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class DataReader : IDataReader
    {
        IHostEnvironment env;
        public DataReader(IHostEnvironment env)
        {
            this.env = env;
        }

        private string GetImageUrl(int productId)
        {
            string host = "https://localhost:6002/api.pizzeria/images"; //read from appsettings! 

            string subdirectoryName;
            int length = productId.ToString().Length;
            int divider = (int)Math.Pow(10 ,(length - 1));

            int categoryId = productId / divider;                              // category is defined by the first digit in id number
            int imageId = productId % divider;      // remained digits are the image id
            string imageName = imageId.ToString() + ".avif";

            switch (categoryId)
            {
                case 1:
                    subdirectoryName = "pizza";
                    break;
                case 2:
                    subdirectoryName = "drinks";
                    break;
                default:
                    subdirectoryName = string.Empty;
                    break;
            }

            string path = host + "/" + subdirectoryName + "/" + imageName;

            return path;
        }

        public List<Product>? GetProducts(string productCategory)
        {
            string directoryPath = "Data/json";
            string fileName;
            switch (productCategory)
            {
                case "pizza":
                    fileName = "pizza.json";
                    break;
                case "drinks":
                    fileName = "drinks.json";
                    break;
                default:
                    fileName = string.Empty;
                    break;
            }

            string path = Path.Combine(env.ContentRootPath, directoryPath, fileName);

            if (!File.Exists(path)) return null;

            string dataAsJson = File.ReadAllText(path);

            // add image urt to data
            List<Product>? products = JsonConvert.DeserializeObject<List<Product>>(dataAsJson);

            if (products == null) return null;

            products.ForEach(product =>
            {
                string imageUrl = GetImageUrl(product.Id);
                product.Image = imageUrl;
            });

            return products;
        }

        public Product? GetProduct(string productCategory, int productId)
        {
            List<Product>? products = GetProducts(productCategory);

            if (products == null) return null;

            Product? product = products.Where(p => p.Id == productId).FirstOrDefault();

            return product;
        }


        public string GetImagePath(string productCategory, string productId)
        {
            string directoryPath = "Data/images";
            return Path.Combine(env.ContentRootPath, directoryPath, productCategory, productId);
        }

        public List<string>? GetCities()
        {
            string dirName = "Data/json";
            string fileName = "cities.json";
            string path = Path.Combine(env.ContentRootPath, dirName, fileName);

            if (!File.Exists(path)) return null;
            
            string citiesAsJson = File.ReadAllText(path);

            List<string>? cities = JsonConvert.DeserializeObject<List<string>>(citiesAsJson);
            return cities;
        }

        public List<Pizzeria>? GetPizzerias(string city)
        {
            string dirName = "Data/json";
            string fileName = "pizzerias.json";
            string path = Path.Combine(env.ContentRootPath, dirName, fileName);

            if (!File.Exists(path)) return null;

            string pizzeriasAsJson = File.ReadAllText(path);

            List<Pizzeria>? pizzerias = JsonConvert.DeserializeObject<List<Pizzeria>>(pizzeriasAsJson);
            
            if (pizzerias==null) return null;

            pizzerias = pizzerias.Where(p => p.City == city).ToList();
            return pizzerias;
        }
    }
}
