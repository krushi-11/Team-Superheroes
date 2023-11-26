using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SuperHeroes.WebSite.Models;
using SuperHeroes.WebSite.Services;

namespace SuperHeroes.WebSite.Pages
{
    /// <summary>
    /// Index page model for homepage
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Constructor for Index model
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }
        /// <summary>
        /// Property to access JsonFileProductService
        /// </summary>
        public JsonFileProductService ProductService { get; }
        
        /// <summary>
        /// Property to store a collection of product models
        /// </summary>
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// OnGet method called when the page is requested
        /// </summary>
        public void OnGet()
        {
            /// Get a list of products from the ProductService
            Products = ProductService.GetProducts();
        }
    }
}