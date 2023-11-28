using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SuperHeroes.WebSite.Pages;
using System.Diagnostics;

namespace SuperHeroes.WebSite.Tests.Pages
{
    [TestFixture]
    public class PageNotFoundModelTests
    {
        private Mock<ILogger<ErrorModel>> _loggerMock;
        private PageNotFoundModel _pageModel;

        [SetUp]
        public void Setup()
        {
            // Arrange: Create a mock logger for the page model
            _loggerMock = new Mock<ILogger<ErrorModel>>();

            // Act: Instantiate PageNotFoundModel with the mock logger
            _pageModel = new PageNotFoundModel(_loggerMock.Object);
        }

        
    }
}
