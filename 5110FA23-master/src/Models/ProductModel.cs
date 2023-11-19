using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// The Product class represents an individual product in the application 
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Getter and Setter for IdModel
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Getter and Setter for MakerModel
        /// </summary>
        public string Maker { get; set; } 

        /// <summary>
        /// Gets or sets the URL to the product's image. Must not be null and must be valid URl.
        /// </summary>
        [Required(ErrorMessage = "Image is required.")]
        [Url(ErrorMessage = "Invalid URL for Image.")]
        [JsonPropertyName("img")]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the URL to additional details about the product. Must not be null and must be a valid URL.
        /// </summary>
        [Required(ErrorMessage = "Url is required.")]
        [Url(ErrorMessage = "Invalid URL.")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the title of the product. Must not be null and limited to 50 characters
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(50, ErrorMessage = "Title must not exceed 50 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Getter and Setter for DescriptionModel
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Regex Code to Limit Price to be only Positive
        /// </summary>
        [Range(0, float.MaxValue, ErrorMessage = "Price should be only Positive.")]
        public float Price { get; set; } 

        /// <summary>
        /// Regex Code to Ensure Stock is a Non-Negative integer
        /// </summary>
        [RegularExpression(@"^\d+$", ErrorMessage = "Stock must be Positive and Non-Decimal Number.")]
        public int Stock { get; set; } 

        /// <summary>
        /// Getter and Setter for RatingsModel
        /// </summary>
        public int[] Ratings { get; set; }

        // Store the Comments entered by the users on this product
        public List<CommentModel> CommentList { get; set; } = new List<CommentModel>();

        /// <summary>
        /// Getter and Setter of ProductType (enum)
        /// </summary>
        public ProductTypeEnum ProductType { get; set; } = ProductTypeEnum.Undefined;

        /// <summary>
        /// Serializes it to a String to Json
        /// </summary>
        /// <returns></returns>
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);
    }
}