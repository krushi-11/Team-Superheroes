using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        public string Id { get; set; } // IdModel of the Product

        public string Maker { get; set; } // MakerModel of the Product

        [Required(ErrorMessage = "Image is required.")] // Ensure Image is not null
        [Url(ErrorMessage = "Invalid URL for Image.")] // Ensure Image is a valid URL
        [JsonPropertyName("img")]
        public string Image { get; set; } // ImageModel of the Product

        [Required(ErrorMessage = "Url is required.")] // Ensure Url is not null
        [Url(ErrorMessage = "Invalid URL.")] // Ensure Url is a valid URL
        public string Url { get; set; } // UrlModel of the Product

        [Required(ErrorMessage = "Title is required.")] // Ensure Title is not null
        [MaxLength(50, ErrorMessage = "Title must not exceed 50 characters.")] // Limit Title to only 50 Characters
        public string Title { get; set; } // TitleModel of the Product

        public string Description { get; set; } // DescriptionModel of the Product

        [Range(0, float.MaxValue, ErrorMessage = "Price should be only Positive.")] // Limit Price to be only Positive
        public float Price { get; set; } // PriceModel of the Product

        [RegularExpression(@"^\d+$", ErrorMessage = "Stock must be Positive and Non-Decimal Number.")] // Ensure Stock is a non-negative integer
        public int Stock { get; set; } // StockModel of the Product

        public int[] Ratings { get; set; } // RatingsModel of the Product

        //Getter and Setter of ProductType (enum)
        public ProductTypeEnum ProductType { get; set; } = ProductTypeEnum.Undefined;

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);
    }
}