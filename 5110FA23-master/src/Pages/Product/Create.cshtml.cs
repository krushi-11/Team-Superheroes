using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class CreateModel : PageModel
    {
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }


        [BindProperty]
        public ProductModel Product { get; set; }
        public IActionResult OnGet()
        {
            Product = ProductService.CreateData();
            return RedirectToPage("./Update", new { Id = Product.Id });
        }

        public IActionResult OnPost()
        {
            // Update the product title using the service
            ProductService.CreateData();

            // Redirect to a confirmation page or a product list page
            return RedirectToPage("./Index");

        }
    }
}
