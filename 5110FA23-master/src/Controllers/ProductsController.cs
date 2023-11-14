using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class ProductsController : ControllerBase
    {
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        public JsonFileProductService ProductService { get; }

        /// Get all Products from Json File
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetProducts();
        }

        /// Requesting Rating from User
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);
            
            return Ok();
        }

        /// ProductModel To Request Rating from User
        public class RatingRequest
        {
            public string ProductId { get; set; }
            public int Rating { get; set; }
        }
    }
}