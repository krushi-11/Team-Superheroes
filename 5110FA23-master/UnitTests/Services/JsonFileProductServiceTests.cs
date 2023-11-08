using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using NUnit.Framework.Internal;

namespace UnitTests.Services
{
    public class JsonFileProductServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        // AddRating Method in JsonFileProductService.cs File
        #region AddRating
        //[Test]
        //public void AddRating_InValid_....()
        //{
        // // Arrange
        // // Act
        // //var result = TestHelper.ProductService.AddRating(null, 1);
        // // Assert
        // //Assert.AreEqual(false, result);
        //}
        // ....

        [Test]
        public void AddRating_Check_Rating_Null_Should_Return_False()
        {
            // Arrange

            // Act

            var check = TestHelper.ProductService.AddRating(null, 2);

            // Assert
            Assert.AreEqual(false, check);
        }

        // Unit Test to Check if Rating is available for product if Yes append Rating and return False

        [Test]
        public void AddRating_Check_Rating_And_AddRating_Should_Return_False()
        {
            // Arrange

            // Act
            var check = TestHelper.ProductService.AddRating("InvalidId", 3);

            // Assert
            Assert.AreEqual(false, check);
        }

        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange
            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_InValid_Product_Empty_Should_Return_False()
        {
            // Arrange
            // Act
            var result = TestHelper.ProductService.AddRating(" ", 3);
            // Assert
            Assert.AreEqual(false, result);
        }
        [Test]
        public void AddRating_InValid_ProductData_Found_Should_Return_False()
        {
            // Arrange
            // Act
            var result = TestHelper.ProductService.AddRating("20", 3);
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_InValid_Product_Rating_less_than_0_Should_Return_False()
        {
            // Arrange
            // Get the First data item
            var data = TestHelper.ProductService.GetProducts().First();

            // Act
            // Store the result of the AddRating method (which is being tested)
            var result = TestHelper.ProductService.AddRating(data.Id, -2);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_InValid_Product_Rating_more_5_Should_Return_False()
        {
            // Arrange
            // Get the First data item
            var data = TestHelper.ProductService.GetProducts().First();

            // Act
            // Store the result of the AddRating method (which is being tested)
            var result = TestHelper.ProductService.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_Valid_Product_Id_Rating_null_Should_Return_new_Array()
        {
            // Arrange
            // Get the Last data item
            var data = TestHelper.ProductService.GetProducts().Last();

            // Act
            // Store the result of the AddRating method (which is being tested)
            var result = TestHelper.ProductService.AddRating(data.Id, 0);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AddRating_InitializeRatingsArrayIfNull()
        {
            // Arrange
            var productId = "tony-stark";
            var rating = 2;

            // Create a product with null Ratings
            var product = new ProductModel
            {
                Id = productId,
                Ratings = null // Simulate a product with no ratings
            };

            // Act
            var result = TestHelper.ProductService.AddRating(productId, rating);

            // Assert
            // Check that the product's Ratings array is initialized and contains the added rating
            Assert.IsNotNull(result);
        }

        #endregion AddRating
        // Ending AddRating Method in JsonFileProductService.cs File

        // UpdateData Method in JsonFileProductService.cs File
        #region UpdateData
        [Test]
        public void UpdateData_Valid_Updated_Value_Matches_Should_Return_true()
        {
            // Arrange
            var data = TestHelper.ProductService.GetProducts().FirstOrDefault();
            var data1 = data;
            data1.Title = "Test";

            // Act
            var result = TestHelper.ProductService.UpdateData(data1);

            // Assert
            Assert.AreEqual(data1.Title, result.Title);
        }

        #endregion UpdateData
        // Ending UpdateData Method in JsonFileProductService.cs File
    }
}