using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        public string Id { get; set; } // IdModel of the Product
        public string Maker { get; set; } // MakerModel of the Product
        
        [JsonPropertyName("img")]
        public string Image { get; set; } // ImageModel of the Product
        public string Url { get; set; } // UrlModel of the Product
        public string Title { get; set; } // TitleModel of the Product
        public string Description { get; set; } // DescriptionModel of the Product
        public float Price { get; set; } // PriceModel of the Product
        public int Stock { get; set; } // StockModel of the Product
        public int[] Ratings { get; set; } // RatingsModel of the Product

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);
    }
}