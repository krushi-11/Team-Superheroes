using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Page model for the index page of the product section.
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Default Constructor for IndexModel.
        /// </summary>
        /// <param name="productService"></param>The service responsible for providing product data.
        public IndexModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Data Servicefor retrieving product information
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Collection of products retrieved from the data service
        /// </summary>
        public IEnumerable<ProductModel> Products { get; private set; }

        // REST OnGet, return all data
        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}