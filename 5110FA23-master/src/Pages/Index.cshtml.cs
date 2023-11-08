using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    //Index page model for homepage
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        //Constructor for Index model
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }
        //Property to access JsonFileProductService
        public JsonFileProductService ProductService { get; }
        
        // Property to store a collection of product models
        public IEnumerable<ProductModel> Products { get; private set; }

        // OnGet method called when the page is requested
        public void OnGet()
        {
            // Get a list of products from the ProductService
            Products = ProductService.GetProducts();
        }
    }
}