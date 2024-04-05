using Microsoft.AspNetCore.Http.Extensions;
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
            int categoryId = productId / 100;   // category is defined by the first digit in id number
            int imageId = productId % 100;      // remained digits are the image id
            string imageName = imageId.ToString() + ".avif";

            switch (categoryId)
            {
                case 1:
                    subdirectoryName = "pizza";
                    break;
                default:
                    subdirectoryName = string.Empty;
                    break;
            }

            string path = host + "/" + subdirectoryName + "/" + imageName;

            return path;
        }

        public string GetJsonData(string product)
        {
            string directoryPath = "Data/json";
            string fileName;
            switch (product)
            {
                case "pizza":
                    fileName = "pizza.json";
                    break;
                default:
                    fileName = string.Empty;
                    break;
            }

            string path = Path.Combine(env.ContentRootPath, directoryPath, fileName);

            if (!File.Exists(path)) return string.Empty;

            string dataAsJson = File.ReadAllText(path);

            // add image urt to data
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(dataAsJson) ?? new List<Product>();
            products.ForEach(product =>
            {
                string imageUrl = GetImageUrl(product.Id);
                product.Image = imageUrl;
            });

            string dataWithImages = JsonConvert.SerializeObject(products);

            return dataWithImages;
        }



        public string GetImagePath(string productCategory, string productId)
        {
            string directoryPath = "Data/images";
            return Path.Combine(env.ContentRootPath, directoryPath, productCategory, productId);
        }
    }
}
