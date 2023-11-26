using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperHeroes.WebSite.Models;
using SuperHeroes.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroes.WebSite.Pages.Product
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
        public IActionResult OnGet(string id)
        {
            Product = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
            if (Product == null)
            {
                ModelState.AddModelError("missing", "Not found error");
                // An error message
                return RedirectToPage("NewErrorPage");
            }

            return Page();
        }
    }
}