using System.Linq;
using SuperHeroes.WebSite.Pages.Product;
using SuperHeroes.WebSite.Services;
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
        /// <summary>
        /// Test initialize
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
        public static DeleteModel pageModel;

        [SetUp]
        public void TestInitialize()

        {
            // Setting up the default HTTP context
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            // Initializing the ModelState
            modelState = new ModelStateDictionary();

            // Creating an ActionContext for the page
            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            // Setting up the model metadata provider and related objects
            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            // Creating a PageContext with the provided action context
            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            ///creating a mockWebHostEnvironment
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            ///creating a MockLoggerDirect
            var MockLoggerDirect = Mock.Of<ILogger<DeleteModel>>();
            JsonFileProductService productService = new JsonFileProductService(mockWebHostEnvironment.Object);
            pageModel = new DeleteModel(productService)
            {
                // Additional setup for the pageModel can be added here if necessary
            };
        }

        #endregion TestSetup

        // Delete Data Method in JsonFileProductService File
        #region DeleteData

        /// <summary>
        /// if the product is invalid, it should return false
        /// </summary>
        [Test]
        public void DeleteData_Invalid_Product_Should_Return_False()
        {
            ///creating variable productId and response
            var productId = "Test";
            var response = pageModel.ProductService.DeleteData(productId);

            Assert.IsFalse(response);

        }

        /// <summary>
        /// If the product is valid, it removes the product and returns true
        /// </summary>
        [Test]
        public void DeleteData_Product_Should_Return_True()
        {
            ///creating variables
            var originalData = File.ReadAllText("../../../../src/bin/Debug/net7.0/wwwroot/data/products.json");
            string productId = "steven-strange";
            var products = pageModel.ProductService.GetProducts().ToList();
            var productToDelete = products.FirstOrDefault(x => x.Id.Equals(productId));
            var result = pageModel.ProductService.DeleteData(productId);

            File.WriteAllText("../../../../src/bin/Debug/net7.0/wwwroot/data/products.json", originalData);
            Assert.IsTrue(result);

        }

        #endregion DeleteData
        // Ending Delete Data Method in JsonFileProductService File

        // OnGet Method in Delete.cshtml.cs file
        #region OnGet

        /// <summary>
        /// Unit test for checking if the valid product is returned
        /// </summary>
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

        /// <summary>
        /// Unit test for checking if an invalid product doesn't return any products
        /// </summary>
        [Test]
        public void OnGet_InValid_Should_Not_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet("Test");

            // Assert
            Assert.IsNull(pageModel.Product);

            // Reset
            pageModel.ModelState.Clear();
        }

        /// <summary>
        /// Unit Test for checking cancel button works or not
        /// </summary>
        [Test]
        public void OnGet_If_Cancel_Button_Redirects_To_Index_Page_Should_Return_True()
        {
            // Arrange

            // Act
            pageModel.OnGet("bruce-banner");
            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        #endregion OnGet
        // Ending OnGet Method in Delete.cshtml.cs file

        // OnPost Method in Delete.cshtml.cs file
        #region OnPost

        /// <summary>
        /// If the Product is Valid, it should return true
        /// </summary>
        [Test]
        public void OnPost_Valid_Should_Return_Products()
        {
            ///creating variable originalData
            var originalData = File.ReadAllText("../../../../src/bin/Debug/net7.0/wwwroot/data/products.json");
            // Arrange
            pageModel.OnGet("steven-strange");

            // Act
            ///creating variable result
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            File.WriteAllText("../../../../src/bin/Debug/net7.0/wwwroot/data/products.json", originalData);
            Assert.AreEqual(true, result.PageName.Contains("Index"));

            // Reset
            pageModel.ModelState.Clear();
        }

        /// <summary>
        /// Unit Test for checking if an invalid model returns to the page
        /// </summary>
        [Test]
        public void OnPost_InValid_Model_Not_Valid_Return_Page()
        {
            // Arrange
            pageModel.Product = new SuperHeroes.WebSite.Models.ProductModel
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

            // Reset
            pageModel.ModelState.Clear();
        }

        #endregion OnPost
        // Ending OnPost Method in Delete.cshtml.cs file
    }
}