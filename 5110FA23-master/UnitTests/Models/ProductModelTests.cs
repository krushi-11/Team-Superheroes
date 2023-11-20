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
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class ProductModelTests
    {
        /// <summary>
        /// Test initialize
        /// </summary>
        #region TestSetup
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static ProductModelTests pageModel;

        [SetUp]
        public void TestInitialize()
        {
            // set up the default HTTP context
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            modelState = new ModelStateDictionary();

            //Create an action context for testing
            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            // create a page context for testing
            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            //set up the web host environment for testing
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            //set up the logger and product service for testing
            var MockLoggerDirect = Mock.Of<ILogger<ProductModelTests>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            //create an instance of the test class
            pageModel = new ProductModelTests()
            {
            };
        }

        #endregion TestSetup

        // Testing the ProductModel.cs file
        #region ModelTests

        /// <summary>
        /// To String should return a Json String
        /// </summary>
        [Test]
        public void ToString_ReturnsJsonString()
        {
            // Arrange
            var product = new ProductModel
            {
                Id = "123",
                Maker = "ContosoCrafts",
                Image = "image.jpg",
                Url = "product/123",
                Title = "Sample Product",
                Description = "A sample product description.",
                Price = 9.99f,
                Stock = 100,
                Ratings = new int[] { 4, 5, 4, 5, 3 }
            };

            // Act
            string json = product.ToString();

            // Assert
            // Validate that the string is a valid JSON string.
            Assert.DoesNotThrow(() => System.Text.Json.JsonDocument.Parse(json));
        }

        #endregion ModelTests
        // Ending Testing in the ProductModel.cs file
    }
}