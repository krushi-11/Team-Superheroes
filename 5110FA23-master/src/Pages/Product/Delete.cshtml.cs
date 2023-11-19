using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using System.Linq;


namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Page model for deleting product details.
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// Data service for managing product information.
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Default Construtor
        /// </summary>
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Getting the Product from ProductModel
        /// </summary>
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// On Get Method to fetch product details by Id
        /// </summary>
        public void OnGet(string id)

        {
            Product = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
        }

        /// <summary>
        /// OnPost Request to Delete Data
        /// </summary>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) // State Validation
            {
                // Handle the case where ModelState is not valid
                return Page();

            }
            // Update the product title using the service
            ProductService.DeleteData(Product.Id);

            // Redirect to a confirmation page or a product list page
            return RedirectToPage("./Index");
        }
    }
}