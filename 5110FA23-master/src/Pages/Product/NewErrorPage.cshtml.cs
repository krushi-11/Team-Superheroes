using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// The ErrorModel class represents the model for the Error Razor Page.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class NewErrorPageModel : PageModel
    {
        // Property to store the current RequestId.
        public string RequestId { get; set; }

        // Property to determine whether to show the RequestId in the UI.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Logger instance for logging errors and messages.
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// Constructor for the ErrorModel class, injecting the logger.
        /// </summary>
        /// <param name="logger"></param>
        public NewErrorPageModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handler for the HTTP GET request to the Error Page.
        /// </summary>
        public void OnGet()
        {
            // Retrieve the current RequestId from the activity or generate a new one.
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}