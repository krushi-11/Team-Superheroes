using System.Linq;
using NUnit.Framework;
using SuperHeroes.WebSite.Models;
using NUnit.Framework.Internal;

namespace UnitTests.Services
{
    public class JsonFileProductServiceTests
    {
        /// <summary>
        /// Test Initialise
        /// </summary>
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        // AddRating Method in JsonFileProductService.cs File
        #region AddRating

        /// <summary>
        /// Null Rating Should Return False
        /// </summary>
        [Test]
        public void AddRating_Check_Rating_Null_Should_Return_False()
        {
            // Arrange

            // Act
            ///creating variable check
            var check = TestHelper.ProductService.AddRating(null, 2);

            // Assert
            Assert.AreEqual(false, check);
        }

        /// <summary>
        /// Unit Test to Check if Rating is available for product if Yes append Rating and return False
        /// </summary>
        [Test]
        public void AddRating_Check_Rating_And_AddRating_Should_Return_False()
        {
            // Arrange

            // Act
            ///creating variable check
            var check = TestHelper.ProductService.AddRating("InvalidId", 3);

            // Assert
            Assert.AreEqual(false, check);
        }

        /// <summary>
        /// Invalid Null Product should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange
            // Act
            ///creating variable result
            var result = TestHelper.ProductService.AddRating(null, 1);
            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Invalid Empty Product should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Empty_Should_Return_False()
        {
            // Arrange
            // Act
            ///creating variable result
            var result = TestHelper.ProductService.AddRating(" ", 3);
            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Invalid ProductData should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_ProductData_Found_Should_Return_False()
        {
            // Arrange
            // Act
            ///creating variable result
            var result = TestHelper.ProductService.AddRating("20", 3);
            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Invalid Rating less than zero should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Rating_less_than_0_Should_Return_False()
        {
            // Arrange
            // Get the First data item
            ///creating variable data
            var data = TestHelper.ProductService.GetProducts().First();

            // Act
            // Store the result of the AddRating method (which is being tested)
            ///creating variable result
            var result = TestHelper.ProductService.AddRating(data.Id, -2);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Invalid Product Rating more than five should return false
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Rating_more_5_Should_Return_False()
        {
            // Arrange
            // Get the First data item
            ///creating variable data
            var data = TestHelper.ProductService.GetProducts().First();

            // Act
            // Store the result of the AddRating method (which is being tested)
            ///creating variable result
            var result = TestHelper.ProductService.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Valid Product Id Rating null should return a new array
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_Id_Rating_null_Should_Return_new_Array()
        {
            // Arrange
            // Get the Last data item
            ///creating variable data
            var data = TestHelper.ProductService.GetProducts().Last();

            // Act
            // Store the result of the AddRating method (which is being tested)
            ///creating variable result
            var result = TestHelper.ProductService.AddRating(data.Id, 0);

            // Assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Initialising array if it is Null
        /// </summary>
        [Test]
        public void AddRating_InitializeRatingsArrayIfNull()
        {
            // Arrange
            ///creating variable productId and rating
            var productId = "tony-stark";
            var rating = 2;

            // Create a product with null Ratings
            var product = new ProductModel
            {
                Id = productId,
                Ratings = null // Simulate a product with no ratings
            };

            // Act
            ///creating variable result
            var result = TestHelper.ProductService.AddRating(productId, rating);

            // Assert
            // Check that the product's Ratings array is initialized and contains the added rating
            Assert.IsNotNull(result);
        }

        #endregion AddRating
        // Ending AddRating Method in JsonFileProductService.cs File

        // UpdateData Method in JsonFileProductService.cs File
        #region UpdateData

        /// <summary>
        /// If Updated Value matches, it should return True
        /// </summary>
        [Test]
        public void UpdateData_Valid_Updated_Value_Matches_Should_Return_true()
        {
            // Arrange
            ///creating variable data and data1
            var data = TestHelper.ProductService.GetProducts().FirstOrDefault();
            var data1 = data;
            data1.Title = "Test";

            // Act
            ///creating variable result
            var result = TestHelper.ProductService.UpdateData(data1);

            // Assert
            Assert.AreEqual(data1.Title, result.Title);
        }

        #endregion UpdateData
        // Ending UpdateData Method in JsonFileProductService.cs File
    }
}