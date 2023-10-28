using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Maker { get; set; }
        
        [JsonPropertyName("img")]
        public string Image { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; } // Price of items
        public int Stock { get; set; } // Number of items left in stock
        public int[] Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);

 
    }
}