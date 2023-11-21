using ContosoCrafts.WebSite.Services;
using NUnit.Framework;
using Bunit;
using ContosoCrafts.WebSite.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using System.IO;

namespace UnitTests.Components
{

    public class ProductListTests : Bunit.TestContext
    {
        [Test]
        public void SelectProduct_InValidId_returns_false()
        {
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_t-challa";

            var page = RenderComponent<ProductList>();

            var buttonList = page.FindAll("Button");

            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            button.Click();

            var pageMarkup = page.Markup;
            Assert.AreEqual(false, pageMarkup.Contains("Testing"));

        }


    }




}