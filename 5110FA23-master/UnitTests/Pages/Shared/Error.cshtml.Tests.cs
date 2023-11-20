using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;
using ContosoCrafts.WebSite.Services;

namespace UnitTests.Pages.Shared
{
    /// <summary>
    /// Class for testing Error Razor Page
    /// </summary>
    public class ErrorTests
    {
        /// <summary>
        /// Test Initialise
        /// </summary>
        #region TestSetup
        // Common setup for testing
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        /// <summary>
        /// Instance of the ErrorModel for testing
        /// </summary>
        public static ErrorModel pageModel;

        // Test initialization method
        [SetUp]
        public void TestInitialize()
        {
            // Setting up the HttpContext and other necessary components for testing
            httpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
                //RequestServices = serviceProviderMock.Object,
            };
            httpContextDefault.HttpContext.TraceIdentifier = "trace";

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
                HttpContext = httpContextDefault
            };

            ///creating a mockWebHostEnvironment
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            ///creating a MockLoggerDirect
            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();

            // Creating a JsonFileProductService for testing
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            // Creating an instance of ErrorModel for testing
            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = pageContext,
                TempData = tempData,
            };
        }

        #endregion TestSetup

        // OnGet Method in Error.cshtml.cs file
        #region OnGet

        /// <summary>
        /// On Get Valid Activity should return a requestID 
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange
            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(activity.Id, pageModel.RequestId);
        }

        /// <summary>
        /// Test for invalid activity (null)
        /// </summary>
        [Test]
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("trace", pageModel.RequestId);
            Assert.AreEqual(true, pageModel.ShowRequestId);
        }

        #endregion OnGet
        // Ending OnGet Method in Error.cshtml.cs file
    }
}