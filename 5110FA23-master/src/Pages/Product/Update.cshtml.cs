using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class UpdateModel : PageModel
    {
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        public UpdateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }


        [BindProperty]
        public ProductModel Product { get; set; }

        public void OnGet(string id)
        {
            Product = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Handle the case where ModelState is not valid
                return Page();
            }
            // Update the product title using the service
            ProductService.UpdateData(Product);

            // Redirect to a confirmation page or a product list page
            return RedirectToPage("./Index");

        }

    }
}
