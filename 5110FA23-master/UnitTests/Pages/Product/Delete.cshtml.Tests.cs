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
    public class DeleteTests
    {
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static DeleteModel pageModel;

        [SetUp]
        public void TestInitialize()

        {
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

        // Unit test for DeleteData if product is invalid then return false

        [Test]
        public void DeleteData_Invalid_Product_Should_Return_False()
        {
            var productId = "Test";
            var response = pageModel.ProductService.DeleteData(productId);

            Assert.IsFalse(response);

        }

        // Unit test for DeleteData if product is valid it removes the product and return true

        [Test]
        public void DeleteData_Product_Should_Return_True()
        {
            string productId = "thunder-god";
            var result = pageModel.ProductService.DeleteData(productId);
            Assert.IsTrue(result);
         
        }
        #endregion DeleteData

        #region OnGet
        [Test]
        /// <summary>
        /// Test that's loading the update page returns a non-empty list of products
        /// </summary>
        /// Wrote Unit test for OnGet request
        public void OnGet_Valid_Should_Return_Product()
        {
            // Arrange

            // Act
            pageModel.OnGet("bruce-banner");
            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);

            // Reset
            // This should remove the error we added
            pageModel.ModelState.Clear();

        }

        [Test]
        public void OnGet_InValid_Should_Not_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet("Test");

            // Assert
            Assert.IsNull(pageModel.Product);
        }

        #endregion OnGet

        #region OnPost
        [Test]
        public void OnPost_Valid_Should_Return_Products()
        {
            // Arrange
            pageModel.OnGet("steve-rogers");

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        [Test]
        public void OnPost_InValid_Model_Not_Valid_Return_Page()
        {
            // Arrange
            pageModel.Product = new ContosoCrafts.WebSite.Models.ProductModel
            {
                Id = "testId",
                Title = "Title",
                Description = "Description",
                Url = "url",
                Image = "image"
            };

            // Force an invalid error state
            pageModel.ModelState.AddModelError("Test", "Test error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }

        #endregion OnPost
    }
}




