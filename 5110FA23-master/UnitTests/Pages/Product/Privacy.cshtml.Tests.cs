using ContosoCrafts.WebSite.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace UnitTests.Pages
{
    [TestFixture]
    public class PrivacyModelTests
    {
        [Test]
        public void OnGet_ShouldReturnPageResult()
        {
            /// Arrange
            var loggerMock = new Mock<ILogger<PrivacyModel>>();
            var privacyModel = new PrivacyModel(loggerMock.Object);

            /// Act
            privacyModel.OnGet();

            /// Assert
            var result = privacyModel.Page();
            Assert.IsInstanceOf<PageResult>(result);
        }
    }
}