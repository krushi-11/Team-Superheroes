using System.Linq;

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

using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;

namespace UnitTests.Pages.Product.Index
{
    public class IndexTests
    {
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

        public static IndexModel pageModel;

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

            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new IndexModel(productService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        [Test]
        /// <summary>
        /// Tests that loading the index page returns a non-empty list of products
        /// </summary>
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            // Are there any in existence?
            Assert.AreEqual(true, pageModel.Products.ToList().Any());
        }
        //Unit Test to check whether delete button redirects to the delete page or not
        [Test]
        public void OnGet_If_Delete_Button_Redirects_Should_True()
        {
            //Arrange

            //Act
            pageModel.OnGet();

            //Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            //Are they redirecting to correct page?
            Assert.AreEqual(true, pageModel.Products.ToList().Any());
        }
        #endregion OnGet


        #region AddRating

        // Unit Test case for AddRating if rating for a product is null it should return False

        [Test]
        public void AddRating_Check_Rating_Null_Should_Return_False()
        {
            // Arrange

            // Act

            var check = pageModel.ProductService.AddRating(null, 2);

            // Assert
            Assert.AreEqual(false, check);
        }

        // Unit Test to Check if Rating is available for product if Yes append Rating and return False

        [Test]
        public void AddRating_Check_Rating_And_AddRating_Should_Return_False()
        {
            // Arrange

            // Act
            var check = pageModel.ProductService.AddRating("InvalidId", 3);

            // Assert
            Assert.AreEqual(false, check);
        }

        #endregion AddRating

    }
}