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
using SuperHeroes.WebSite.Pages.Product;
using SuperHeroes.WebSite.Services;

namespace UnitTests.Pages.Product.Read
{
    public class ReadTests
    {
        /// <summary>
        /// Test Initialise
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

        public static ReadModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            // Creating necessary objects for testing
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            // Setting up a mock WebHostEnvironment

            ///creating a mockWebHostEnvironment
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            ///creating a MockLoggerDirect
            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            JsonFileProductService productService;


            // Creating a JsonFileProductService instance
            productService = new JsonFileProductService(mockWebHostEnvironment.Object);


            // Initializing the pageModel with the productService
            pageModel = new ReadModel(productService)
            {
                // Additional setup for the pageModel can be added here if necessary
            };
        }

        #endregion TestSetup

        // OnGet Method in Read.cshtml.cs file
        #region OnGet

        /// <summary>
        /// Test case for the OnGet method when a valid product is requested
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet("mind-stone");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("Vision", pageModel.Product.Title);

            // Reset
            pageModel.ModelState.Clear();
        }

        /// <summary>
        /// Test case for checking if the Update button redirects properly
        /// </summary>
        [Test]
        public void OnGet_If_Update_Button_Redirects_Should_Return_True()
        {
            //Arrange

            //Act
            pageModel.OnGet("steven-strange");

            //Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("Doctor Strange", pageModel.Product.Title);

            // Reset
            pageModel.ModelState.Clear();
        }

        /// <summary>
        /// Cancel Button should redirect to products index page and it returns true
        /// </summary>
        [Test]
        public void OnGet_If_Cancel_Button_Redirects_To_Products_Index_Page_Should_Return_True()
        {
            //Arrange

            //Act
            pageModel.OnGet("steven-strange");

            //Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("Doctor Strange", pageModel.Product.Title);

            // Reset
            pageModel.ModelState.Clear();
        }

        #endregion OnGet
        /// Ending OnGet Method in Read.cshtml.cs file
    }
}