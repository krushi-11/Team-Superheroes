using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using System.Linq;


namespace ContosoCrafts.WebSite.Pages.Product
{
    public class DeleteModel : PageModel
    {
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Default Construtor
        /// </summary>
        
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }


        [BindProperty]

        ///Getting the Product from ProductModel
        public ProductModel Product { get; set; }

        ///On Get Method to fetch product details by Id
        public void OnGet(string id)

        {
            Product = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
        }


        // OnPost Request to Delete Data

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