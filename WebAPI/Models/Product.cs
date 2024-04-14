using System.Collections.Generic;

namespace WebAPI.Models
{

    public class Product
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<string> Ingredients { get; set; }
        public int Price { get; set; }
        public Options Options { get; set; }
        public int Amount { get; set; }
    }


  

 
}
