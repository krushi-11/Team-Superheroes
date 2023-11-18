using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        // Getter and Setter for IdModel
        public string Id { get; set; }

        // Getter and Setter for MakerModel
        public string Maker { get; set; } 

        // Regex Code to Ensure Image is not null
        [Required(ErrorMessage = "Image is required.")]
        // Regex Code to Ensure Image is a valid URL
        [Url(ErrorMessage = "Invalid URL for Image.")]
        // Getter and Setter for ImageModel
        [JsonPropertyName("img")]
        public string Image { get; set; }

        // Regex Code to Ensure Url is not null
        [Required(ErrorMessage = "Url is required.")]
        // Regex Code to Ensure Url is a valid URL
        [Url(ErrorMessage = "Invalid URL.")]
        // Getter and Setter for UrlModel
        public string Url { get; set; }

        // Regex Code to Ensure Title is not null
        [Required(ErrorMessage = "Title is required.")]
        // Regex Code to Limit Title to only 50 Characters
        [MaxLength(50, ErrorMessage = "Title must not exceed 50 characters.")]
        // Getter and Setter for TitleModel
        public string Title { get; set; }

        // Getter and Setter for DescriptionModel
        public string Description { get; set; }

        // Regex Code to Limit Price to be only Positive
        [Range(0, float.MaxValue, ErrorMessage = "Price should be only Positive.")] 
        // Getter and Setter for PriceModel
        public float Price { get; set; } 

        // Regex Code to Ensure Stock is a Non-Negative integer
        [RegularExpression(@"^\d+$", ErrorMessage = "Stock must be Positive and Non-Decimal Number.")]
        // Getter and Setter for StockModel
        public int Stock { get; set; } 

        // Getter and Setter for RatingsModel
        public int[] Ratings { get; set; } 

        //Getter and Setter of ProductType (enum)
        public ProductTypeEnum ProductType { get; set; } = ProductTypeEnum.Undefined;

        //Serializes it to a String to Json
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);
    }
}