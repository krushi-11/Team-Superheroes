using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Page model for the privacy page
    /// </summary>
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// constructor for privacy model
        /// </summary>
        /// <param name="logger"></param>Logger instance for logging privacy-related events
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles HTTP GET requests for the privacy page.
        /// </summary>
        public void OnGet()
        {
            // the OnGet method is intenionally empty as the privacy page has no specific actions
            // Logging or additional logic related to privacy can be added in the future if needed
        }
    }
}