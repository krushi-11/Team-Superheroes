using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using Moq;
using System.Collections.Generic;

namespace UnitTests.Pages.Product.AddRating
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

        [Test]
        public void AddRating_NullProductId_ReturnsFalse()
        {
            // Arrange
            string productId = null;
            int rating = 5;
            // Act
            var result = TestHelper.ProductService.AddRating(productId, rating);
            // Act and Assert
            Assert.IsFalse(result, "Expected AddRating to return false for a null productId.");
        }

        [Test]
        public void AddRating_InValid_Rating_LessRatingThanZero_Should_Return_False()
        {
            // Arrange
            string productId = "InvalidId";
            // Act
            var result = TestHelper.ProductService.AddRating(productId, -3);
            // Assert
            Assert.AreEqual(false, result); 
        }

        [Test]
        public void AddRating_Valid_Rating_Should_Return_True()
        {
            // Arrange
            // Act
            var result = TestHelper.ProductService.AddRating("sailorhg-corsage", 4);
            // Assert
            Assert.AreEqual(true, result);
        }

        #endregion AddRating

    }
}