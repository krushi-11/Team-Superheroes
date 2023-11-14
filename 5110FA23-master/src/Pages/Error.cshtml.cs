using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    // The ErrorModel class represents the model for the Error Razor Page.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        // Property to store the current RequestId.
        public string RequestId { get; set; }

        // Property to determine whether to show the RequestId in the UI.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Logger instance for logging errors and messages.
        private readonly ILogger<ErrorModel> _logger;

        // Constructor for the ErrorModel class, injecting the logger.
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        // Handler for the HTTP GET request to the Error Page.
        public void OnGet()
        {
            // Retrieve the current RequestId from the activity or generate a new one.
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}