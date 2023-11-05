using System.ComponentModel.DataAnnotations;
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

        [MaxLength(50, ErrorMessage = "Title must not exceed 50 characters.")] // Limit Title to only 50 Characters
        public string Title { get; set; } // TitleModel of the Product

        public string Description { get; set; } // DescriptionModel of the Product

        [Range(0, float.MaxValue, ErrorMessage = "Price should be only Positive.")] // Limit Price to be only Positive
        public float Price { get; set; } // PriceModel of the Product

        [Range(0, int.MaxValue, ErrorMessage = "Price must be non-negative.")] // Limit Stock to be only Positive
        public int Stock { get; set; } // StockModel of the Product

        public int[] Ratings { get; set; } // RatingsModel of the Product

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);
    }
}