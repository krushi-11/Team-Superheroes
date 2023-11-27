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
    [TestFixture]
    public class NewErrorPageModelTests
    {
        private Mock<ILogger<ErrorModel>> _loggerMock;
        private NewErrorPageModel _pageModel;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ErrorModel>>();
            _pageModel = new NewErrorPageModel(_loggerMock.Object);
            // Setting up the NewErrorPageModel with a mock logger
            _pageModel = new NewErrorPageModel(Mock.Of<ILogger<ErrorModel>>());
        }

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

        [Test]
        public void ShowRequestId_WithNullOrEmptyRequestId_ReturnsFalse()
        {
            // Arrange

            // Act
            var result = _pageModel.ShowRequestId;

            // Assert
            Assert.IsFalse(result);
        }

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