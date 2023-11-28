using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace SuperHeroes.WebSite.Pages
{
    public class PageNotFoundModel : PageModel
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
        public PageNotFoundModel(ILogger<ErrorModel> logger)
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
