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
using System.IO;

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

        // Delete Data Method in JsonFileProductService File
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
            var originalData = File.ReadAllText("../../../../src/bin/Debug/net7.0/wwwroot/data/products.json");
            string productId = "steven-strange";
            var products = pageModel.ProductService.GetProducts().ToList();
            var productToDelete = products.FirstOrDefault(x => x.Id.Equals(productId));
            var result = pageModel.ProductService.DeleteData(productId);
            /*pageModel.ProductService.CreateData(productToDelete);*/

            File.WriteAllText("../../../../src/bin/Debug/net7.0/wwwroot/data/products.json", originalData);
            Assert.IsTrue(result);

        }

        #endregion DeleteData
        // Ending Delete Data Method in JsonFileProductService File

        // OnGet Method in Delete.cshtml.cs file
        #region OnGet

        [Test]
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

        // Wrote unit test, if invalid product should return null
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
        // Ending OnGet Method in Delete.cshtml.cs file

        // OnPost Method in Delete.cshtml.cs file
        #region OnPost

        // If the Product is Valid return true
        [Test]
        public void OnPost_Valid_Should_Return_Products()
        {
            var originalData = File.ReadAllText("../../../../src/bin/Debug/net7.0/wwwroot/data/products.json");
            // Arrange
            pageModel.OnGet("steven-strange");

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            File.WriteAllText("../../../../src/bin/Debug/net7.0/wwwroot/data/products.json", originalData);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        // Unit Test for If the Product is Not Valid it Return to the Page
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
        // Ending OnPost Method in Delete.cshtml.cs file
    }
}