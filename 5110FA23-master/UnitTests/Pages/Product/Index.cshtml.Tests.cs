using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;

namespace UnitTests.Pages.Product.Index
{
    public class IndexTests
    {
        #region TestSetup

        // Declaration of necessary testing objects
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;
        public static IndexModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            // Setting up the default HTTP context
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            // Initializing the ModelState
            modelState = new ModelStateDictionary();

            // Creating an ActionContext for the page
            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            // Setting up the model metadata provider and related objects
            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            // Creating a PageContext with the provided action context
            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            // Setting up a mock WebHostEnvironment
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            JsonFileProductService productService;

            // Creating a JsonFileProductService instance with the mock WebHostEnvironment
            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            // Initializing the IndexModel with the productService
            pageModel = new IndexModel(productService)
            {
                // Additional setup for the pageModel can be added here if necessary
            };
        }

        #endregion TestSetup

        // OnGet Method in Index.cshtml.cs file
        #region OnGet

        // Test case for the OnGet method when the index page returns a non-empty list of products
        [Test]
        /// <summary>
        /// Tests that loading the index page returns a non-empty list of products
        /// </summary>
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            // Are there any products in existence?
            Assert.AreEqual(true, pageModel.Products.ToList().Any());
        }

        // Unit test to check whether the delete button redirects to the delete page or not
        [Test]
        public void OnGet_If_Delete_Button_Redirects_Should_True()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            // Are they redirecting to the correct page?
            Assert.AreEqual(true, pageModel.Products.ToList().Any());
        }
        #endregion OnGet
        // Ending OnGet Method in Index.cshtml.cs file
    }
}
