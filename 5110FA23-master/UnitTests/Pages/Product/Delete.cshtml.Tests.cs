using System.Linq;
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
using System.Collections.Generic;
using System.IO;
using System.Text.Json;



namespace UnitTests.Pages.Product.Delete
{


    [TestFixture]
    public class ProductServiceTests
    {
        public static DeleteModel pageModel;

        [SetUp]
        public void TestInitialize()

        {
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<DeleteModel>>();
            JsonFileProductService productService= new JsonFileProductService(mockWebHostEnvironment.Object);
            pageModel = new DeleteModel(productService)
            {
            };
        }

        
        #region DeleteData

        // Unit test for DeleteData if productId is invalid then return false

        [Test]
        public void DeleteData_Invalid_Product_Should_Return_False()
        {
            var productId = "Test";
            var response = pageModel.ProductService.DeleteData(productId);

            Assert.IsFalse(response);

        }
        #endregion DeleteData

    }



}




