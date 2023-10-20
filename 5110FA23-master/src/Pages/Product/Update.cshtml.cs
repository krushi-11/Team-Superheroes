using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Text.Json;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class UpdateProductModel : PageModel
    {
        [BindProperty]
        public ProductModel Product { get; set; }

        public IActionResult OnGet()
        {
            // Load the existing product data from the products.json file
            string productJson = System.IO.File.ReadAllText("products.json");
            Product = JsonSerializer.Deserialize<ProductModel>(productJson);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Serialize the modified product to JSON
                string updatedProductJson = JsonSerializer.Serialize(Product, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Write the updated data back to the products.json file
                System.IO.File.WriteAllText("products.json", updatedProductJson);

                // Redirect to a confirmation page or a product list page
                return RedirectToPage("/Product/Index");
            }

            // If ModelState is not valid, return the current page
            return Page();
        }

    }
}
