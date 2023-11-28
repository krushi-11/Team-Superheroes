using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SuperHeroes.WebSite.Pages.Product;
using SuperHeroes.WebSite.Pages;
using System.Diagnostics;

namespace SuperHeroes.WebSite.Tests.Pages.Product
{
    /// <summary>
    /// Unit tests for the NewErrorPageModel class.
    /// </summary>
    [TestFixture]
    public class NewErrorPageModelTests
    {
        private Mock<ILogger<ErrorModel>> _loggerMock;
        private NewErrorPageModel _pageModel;

        /// <summary>
        /// setup method executed before each test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            //Arrange: Create a mock logger for the page model
            _loggerMock = new Mock<ILogger<ErrorModel>>();
            
            //Act: Instantiate NewErrorPageModel with the mock logger
            _pageModel = new NewErrorPageModel(_loggerMock.Object);
            
            // Setting up the NewErrorPageModel with a mock logger
            _pageModel = new NewErrorPageModel(Mock.Of<ILogger<ErrorModel>>());
        }

        /// <summary>
        /// Tests the OnGet method when there is an existing activity
        /// </summary>
        [Test]
        public void OnGet_WithExistingActivity_SetsRequestIdFromActivity()
        {
            // Arrange
            Activity activity = new Activity("TestActivity");
            activity.Start();

            // Act
            _pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.AreEqual(activity.Id, _pageModel.RequestId);
        }

        /// <summary>
        /// Tests the OnGet method when there is a null activity.
        /// </summary>
        [Test]
        public void OnGet_WithNullActivity_SetsRequestIdFromHttpContext()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            httpContext.TraceIdentifier = "TestTraceId";

            // Act
            _pageModel.PageContext = new PageContext { HttpContext = httpContext };
            _pageModel.OnGet();

            // Assert
            Assert.AreEqual(httpContext.TraceIdentifier, _pageModel.RequestId);
        }

        /// <summary>
        /// Tests the ShowRequestId property when RequestId is null or empty
        /// </summary>
        [Test]
        public void ShowRequestId_WithNullOrEmptyRequestId_ReturnsFalse()
        {
            // Arrange

            // Act
            var result = _pageModel.ShowRequestId;

            // Assert
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the ShowRequestId property when RequestId is valid
        /// </summary>
        [Test]
        public void ShowRequestId_WithValidRequestId_ReturnsTrue()
        {
            // Arrange
            _pageModel.RequestId = "TestRequestId";

            // Act
            var result = _pageModel.ShowRequestId;

            // Assert
            Assert.IsTrue(result);
        }
    }
}