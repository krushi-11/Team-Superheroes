using ContosoCrafts.WebSite.Services;
using NUnit.Framework;
using Bunit;
using ContosoCrafts.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Components
{
    /// <summary>
    /// Unit tests for the ProductList component
    /// </summary>
    public class ProductListTests: Bunit.TestContext
    {
        /// <summary>
        /// Test that the method adds a product to the shopping cart
        /// </summary>
        [Test]
        public void AddToCartButtonShouldAddProductToShoppingCart()
        {
           
            // Arrange: To register the JsonFileProductServices
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act: Render the component
            var cut = RenderComponent<ProductList>();

            // Act: Click on the Add to Cart button
            cut.Find(".addtocart").Click();

            // Assert: Verify that the product is added to the shopping cart
            var shoppingCart = cut.Find("#addedToCart");
            Assert.IsTrue(shoppingCart.TextContent.Contains("Added to Cart"));
        }
    }
}