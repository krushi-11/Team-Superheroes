using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Bunit;
using System.Collections.Generic;
using System.Linq;
using ContosoCrafts.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Components
{
    public class ProductListTests
    {
        [Test]
        public void AddToCartButtonShouldAddProductToShoppingCart()
        {
            // Arrange
            using var ctx = new Bunit.TestContext();

            // Register your services, you may need to customize this based on your setup
            ctx.Services.AddSingleton<JsonFileProductService>();

            // Render the component
            var cut = ctx.RenderComponent<ProductList>();

            // Act: Click on the Add to Cart button
            cut.Find(".addtocart").Click();

            // Assert: Verify that the product is added to the shopping cart
            var shoppingCart = cut.Find("ul");
            Assert.IsTrue(shoppingCart.TextContent.Contains("AddedProductTitle"));
        }

        // Add more tests as needed
    }
}