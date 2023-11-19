using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Page Model for creating
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Data service for managing product information
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Getting the Product from ProductModel
        /// </summary>
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// OnPost Method to send data
        /// </summary>
        public IActionResult OnPost()
        {

            if (ModelState.IsValid) ///State Validation Check
            {
                /// Save the product to the database (or in this case, the JSON file)
                ProductService.CreateData(Product);

                /// Redirect to a success or product listing page after saving
                return RedirectToPage("./Index");
            }

            /// Stay on the same page if no file was uploaded or there was an issue
            return Page();
        }
    }
}