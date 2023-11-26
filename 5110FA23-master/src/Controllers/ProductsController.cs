using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SuperHeroes.WebSite.Models;
using SuperHeroes.WebSite.Services;

namespace SuperHeroes.WebSite.Controllers
{
    /// <summary>
    /// API Controller for managing products
    /// </summary>
    [ApiController]
    [Route("[controller]")]

    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Constructor for the ProductsController
        /// </summary>
        /// <param name="productService"></param> The service for managing product data
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Service for managing product data
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Get all the products for the JSON file
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetProducts();
        }

        /// <summary>
        /// Handles HTTP patch requests to update product ratings
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            // Add the provide rating to the specified product.
            ProductService.AddRating(request.ProductId, request.Rating);
            
            //Return a success status.
            return Ok();
        }

        /// <summary>
        /// Model for requesting a rating from the user.
        /// </summary>
        public class RatingRequest
        {
            /// <summary>
            /// gets or see the productID for which the rating is required 
            /// </summary>
            public string ProductId { get; set; }
            
            /// <summary>
            /// Gets or see the rating provided by the user.
            /// </summary>
            public int Rating { get; set; }
        }
    }
}