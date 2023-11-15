using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
        public IndexModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Data Service
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Collection of the Data
        /// </summary>
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// REST OnGet, return all data
        /// </summary>
        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}