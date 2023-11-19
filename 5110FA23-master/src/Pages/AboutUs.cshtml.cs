using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Page model for the AboutUs page 
    /// </summary>
    public class AboutUsModel : PageModel
    { 
        /// <summary>
        /// Logger instance for logging
        /// </summary>
        public readonly ILogger<AboutUsModel> _logger;

        /// <summary>
        /// Constructor for the AboutUsModel
        /// </summary>
        /// <param name="logger"></param> The logger instance for logging
        public AboutUsModel(ILogger<AboutUsModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles HTTP GET requests for the about us page.
        /// </summary>
        public void OnGet()
        {
            //This method is currently empty as there is no specific logic for the AboutUs page
            //Additional logic can be added as needed.
        }
    }
}