using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperHeroes.WebSite.Services;
using SuperHeroes.WebSite.Models;
using System.Linq;

namespace SuperHeroes.WebSite.Pages.Product
{
    /// <summary>
    /// Page model for updating product details
    /// </summary>
    public class UpdateModel : PageModel
    {
        /// <summary>
        /// Data service for managing product information.
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        public UpdateModel(JsonFileProductService productService)
        {
            
            ProductService = productService;
        
        }

        /// <summary>
        /// Getting Product from Product Model
        /// </summary>
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// On Get Method to fetch the first product
        /// </summary>
        public void OnGet(string id)
        {

            Product = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
            if (Product == null)
            {
                ModelState.AddModelError("OnGet", "Update Onget Error");
                // An error message
                RedirectToPage("NewErrorPage");
            }

        }

        /// <summary>
        /// OnPost Method to Update Data
        /// </summary>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) /// State Validation
            {
                /// Handle the case where ModelState is not valid
                return RedirectToPage("NewErrorPage");
            }

            /// Update the product title using the service
            ProductService.UpdateData(Product);

            /// Redirect to a confirmation page or a product list page
            return RedirectToPage("./Index");
        }
    }
}