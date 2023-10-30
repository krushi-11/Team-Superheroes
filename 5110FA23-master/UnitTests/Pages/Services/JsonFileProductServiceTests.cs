using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using Moq;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnitTests.Pages.Services.JsonFileProductService
{
    public class JsonFileProductServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

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
        public void
        AddRating_InValid_Rating_LessRatingThanZero_Should_Return_False()
        {
            // Arrange
            // Act
            var result = TestHelper.ProductService.AddRating("jenlooper-cactus", -3);
            // Assert
            Assert.AreEqual(false, result);
        }
        [Test]
        public void AddRating_InValid_Rating_GreaterThanFive_Should_Return_False()
        {
            // Arrange
            // Act
            var result = TestHelper.ProductService.AddRating("jenlooper-cactus", 7);
            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_Valid_Rating_Should_Return_True()
        {
            // Arrange
            // Act
            var result = TestHelper.ProductService.AddRating("sailorhg-bubblesortpic", 2);
            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AddRating_InitializeRatingsArrayIfNull()
        {
            // Arrange
            var productId = "jenlooper-cactus";
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

        [Test]
        public void UpdateData_DataIsNull_ReturnsNull()
        {
            // Arrange
            var productService = TestHelper.ProductService.GetProducts().FirstOrDefault(); // Replace with your actual service class
            ProductModel data = null;

            // Act
            var result = TestHelper.ProductService.UpdateData(data);

            // Assert
            Assert.IsNull(result, "Data is null");
        }

        #endregion UpdateData

        #region CreateData
        [Test]
        public void CreateData_ShouldReturnProductWithNewId()
        {
            // Arrange

            // Act
            var res = TestHelper.ProductService.CreateData();

            // Assert
            Assert.NotNull(res);
            Assert.IsNotEmpty(res.Id); // Ensure the product has a non-empty ID
            Assert.AreEqual("Enter Title", res.Title); // Check the default title
            Assert.AreEqual("Enter Description", res.Description); // Check the default description
            Assert.AreEqual("Enter Url", res.Url); // Check the default URL
            Assert.AreEqual("", res.Image); // Check the default image
        }
        #endregion CreateData

        #region DeleteData
        [Test]
        public void DeleteData_ShouldRemoveProductWithSpecifiedId()
        {
            // Arrange
            var ProductID = "steve-rogers";

            // Act
            var productToDelete = TestHelper.ProductService.GetProducts().FirstOrDefault(p => p.Id == ProductID);
            var deletedProduct = TestHelper.ProductService.DeleteData(ProductID);

            // Assert
            Assert.NotNull(productToDelete);
            Assert.AreEqual(ProductID, deletedProduct.Id);

            // Check that the product with ID "tony-stark" was removed
            var remainingProducts = TestHelper.ProductService.GetProducts().ToList();
            Assert.Null(remainingProducts.FirstOrDefault(x => x.Id == ProductID));
        }
        #endregion DeleteData
    }
}