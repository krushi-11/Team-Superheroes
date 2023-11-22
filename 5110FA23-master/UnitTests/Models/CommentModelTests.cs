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
    public class CommentModelTests
    {
        /// <summary>
        /// Test initialize
        /// </summary>
        /// 
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

        public static CommentModelTests pageModel;

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
            var MockLoggerDirect = Mock.Of<ILogger<CommentModelTests>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            //create an instance of the test class
            pageModel = new CommentModelTests()
            {
            };
        }

        #endregion TestSetup

        // Testing the CommentModel.cs file
        #region CommentModel

        /// <summary>
        /// Setting the Comment Id
        /// </summary>
        [Test]
        public void CommentModel_Id_Should_Be_Set()
        {
            // Arrange
            // Create a new CommentModel instance
            var comment = new CommentModel();

            // Act
            // Set a custom value for the Id property
            comment.Id = "CustomIdValue";

            // Get the value of the Id property
            var id = comment.Id;

            // Assert
            // Ensure that the Id is set to the custom value
            Assert.AreEqual("CustomIdValue", id);
        }

        /// <summary>
        /// Comment should be set and recieved correctly
        /// </summary>
        [Test]
        public void CommentModel_Comment_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            // Create a new CommentModel instance
            var comment = new CommentModel();

            // Act
            // Set the Comment property
            comment.Comment = "This is a test comment";

            // Get the value of the Comment property
            var retrievedComment = comment.Comment;

            // Assert
            // Ensure that the Comment property is set and retrieved correctly
            Assert.AreEqual("This is a test comment", retrievedComment);
        }

        // Ending Testing in the CommentModel.cs file
        #endregion CommentModel

    }
}