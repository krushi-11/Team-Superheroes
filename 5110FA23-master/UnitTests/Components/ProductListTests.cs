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
using Microsoft.AspNetCore.Hosting;

namespace UnitTests.Components
{
    public class ProductListTests: Bunit.TestContext
    {
        [Test]
        public void AddToCartButtonShouldAddProductToShoppingCart()
        {
           
            // To register the JsonFileProductServices
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Render the component
            var cut = RenderComponent<ProductList>();

            // Act: Click on the Add to Cart button
            cut.Find(".addtocart").Click();

            // Assert: Verify that the product is added to the shopping cart
            var shoppingCart = cut.Find("#addedToCart");
            Assert.IsTrue(shoppingCart.TextContent.Contains("Added to Cart"));
        }
    }
}