using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Page Model for reading product details.
    /// </summary>
    public class ReadModel : PageModel
    {
        /// <summary>
        /// Data middletier
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param> The service for managing product data.
        public ReadModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// The data to show
        /// </summary>
        public ProductModel Product;

        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name="id"></param> The ID of the product to retrieve
        public void OnGet(string id) /// OnGet Request to Get Products by id
        {
            // Retrieve the product with the specified ID from the data service.
            Product = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
        }
    }
}