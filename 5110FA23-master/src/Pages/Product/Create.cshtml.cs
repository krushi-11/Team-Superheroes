using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System;

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
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Save the product to the database (or in this case, the JSON file)
                ProductService.CreateData(Product);

                // Redirect to a success or product listing page after saving
                return RedirectToPage("./Index");
            }

            // Stay on the same page if no file was uploaded or there was an issue
            return Page();
        }
    }
}
