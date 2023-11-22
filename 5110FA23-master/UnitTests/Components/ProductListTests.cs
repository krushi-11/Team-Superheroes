using Bunit;

using NUnit.Framework;

using Microsoft.Extensions.DependencyInjection;

using System.Linq;

using ContosoCrafts.WebSite.Models;

using ContosoCrafts.WebSite.Components;

using ContosoCrafts.WebSite.Services;

using System;


namespace UnitTests.Components.Tests

{

    /// <summary>

    /// This class contains unit tests for the ProductModel

    /// </summary>

    public class ProductListTests : Bunit.TestContext

    {

        #region TestSetup

        public ProductModel[] Products;

        /// <summary>

        /// Test Setup

        /// </summary>

        [SetUp]

        public void TestInitialize()

        {

        }
        #endregion TestSetup

        #region ProductList
        /// <summary>
        /// Test for getting the product list
        /// </summary>
        /// 
        [Test]
        public void ProductList_Default_Should_Return_Content()

        {
            // Arrange

            Services.AddSingleton(TestHelper.ProductService);

            // Act

            // Render the ProductList component

            var page = RenderComponent<ProductList>();

            // Get the Cards retrned

            var result = page.Markup;

            // Assert

            // Check if the markup contains a specific content

            Assert.AreEqual(true, result.Contains("t-challa"));
        }
        #endregion ProductList

        /// <summary>

        /// Test for testing the enabling the filter function

        /// </summary>

        #region FilterData

        [Test]

        public void Enable_Filter_Data_Set_to_True_Should_Return_True()

        {

            // Arrange

            Services.AddSingleton(TestHelper.ProductService);

            var filterButton = "Filter";

            var page = RenderComponent<ProductList>();

            var buttonList = page.FindAll("Button");

            var button = buttonList.First(m => m.OuterHtml.Contains(filterButton));

            // Act

            button.Click();

            var pageMarkup = page.Markup;

            // Assert

            Assert.AreEqual(true, page.Instance.FilterData);

        }
        /// <summary>

        /// Test for updating the filter

        /// </summary>

        [Test]

        public void UpdateFilterType_ShouldUpdateFilterDataType()

        {

            // Arrange

            Services.AddSingleton(TestHelper.ProductService);

            var page = RenderComponent<ProductList>();

            // Act

            var inputField = page.Find("input[type='text']");

            inputField.Change("NewFilterText");

            // Assert

            Assert.AreEqual("NewFilterText", page.Instance.FilterDataString);

        }
    }

    #endregion FilterData
}