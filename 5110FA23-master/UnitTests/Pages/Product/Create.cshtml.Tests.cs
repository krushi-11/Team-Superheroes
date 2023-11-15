using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.Create
{
    public class CreateTests
    {
        /// <summary>
        /// Test initialize
        /// </summary>
        #region TestSetup
        public static IUrlHelperFactory urlHelperFactory; // Factory for URL helper
        public static DefaultHttpContext httpContextDefault; // Default HTTP context
        public static IWebHostEnvironment webHostEnvironment; // Web host environment
        public static ModelStateDictionary modelState; // Dictionary for model state
        public static ActionContext actionContext; // Action context
        public static EmptyModelMetadataProvider modelMetadataProvider; // Model metadata provider
        public static ViewDataDictionary viewData; // View data dictionary
        public static TempDataDictionary tempData; // Temporary data dictionary
        public static PageContext pageContext; // Page context

        public static CreateModel pageModel; // Instance of the CreateModel

        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext(); // Creating a new default HTTP context

            modelState = new ModelStateDictionary(); // Initializing a new model state dictionary

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState); // Creating a new action context

            modelMetadataProvider = new EmptyModelMetadataProvider(); // Initializing an empty model metadata provider
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState); // Creating a new view data dictionary
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>()); // Creating a new temporary data dictionary

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData, // Setting view data
            };

            ///creating variable mockWebHostEnvironment
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>(); // Mocking the web host environment
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment"); // Setting up environment name
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot"); // Setting up web root path
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/"); // Setting up content root path

            ///creating variable MockLoggerDirect
            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>(); // Mocking the logger

            JsonFileProductService productService; // JSON file product service
            productService = new JsonFileProductService(mockWebHostEnvironment.Object); // Initializing JSON file product service

            pageModel = new CreateModel(productService) // Creating a new instance of CreateModel
            {
                // Additional setup if needed
            };
        }
        #endregion TestSetup

        // OnPost Method in Create.cshtml File
        #region OnPost

        /// <summary>
        /// On Posting Invalid Model is Not Valid and should Return Page
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_NotValid_Return_Page()
        {
            // Arrange

            // Act
            pageModel.ModelState.AddModelError("-light", ""); // Adding model error
            var result = pageModel.OnPost() as ActionResult; // Executing OnPost method

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid); // Checking if model state is not valid

            // Reset
            pageModel.ModelState.Clear();
        }

        /// <summary>
        /// On Posting Valid it should return True 
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Return_True()
        {
            // Arrange
            pageModel.Product = new ProductModel // Creating a new ProductModel instance
            {
                Title = "test", // Setting the title
                Image = "test", // Setting the image
                Description = "test", // Setting the description
            };

            // Act
            ///creating variable result
            var result = pageModel.OnPost() as RedirectToPageResult; // Executing OnPost method

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid); // Checking if model state is valid
            Assert.AreEqual(true, result.PageName.Contains("Index")); // Checking if the page name contains "Index"

            // Reset
            pageModel.ModelState.Clear();
        }

        #endregion OnPost
        // Ending OnPost Method in Create.cshtml File
    }
}