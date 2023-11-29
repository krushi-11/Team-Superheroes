using Bunit;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using SuperHeroes.WebSite.Components;
using System.Linq;
using SuperHeroes.WebSite.Services;

namespace UnitTests.Components.Tests
{ 
     /// <summary>
     /// This class contains unit tests for the ProductModel
     /// </summary>
    public class ProductListTests : Bunit.TestContext
    {
    public ProductListTests()
        {
            Services.AddSingleton(TestHelper.ProductService);
        }

        /// <summary>
        /// Test for getting the product list
        /// </summary>
        #region Rating

        
        #endregion Rating

        #region ProductList

        [Test]
        public void ProductList_Default_Should_Return_Content()
        {
            // Act
            var page = RenderComponent<ProductList>();
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("t-challa"));
        }

        #endregion ProductList

        #region FilterData

        /// <summary>
        /// Test for testing the enabling the filter function
        /// </summary>

        [Test]
        public void Enable_Filter_Data_Set_to_True_Should_Return_True()
        {
            // Act
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains("Filter"));
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
            // Act
            var page = RenderComponent<ProductList>();
            var inputField = page.Find("input[type='text']");
            inputField.Change("NewFilterText");

            // Assert
            Assert.AreEqual("NewFilterText", page.Instance.FilterDataString);
        }

        /// <summary>
        /// Test for clearing out the filter text
        /// </summary>
        [Test]
        public void Clear_Filter_Data_Set_to_False_Should_Return_False()
        {
            // Act
            var clearButton = "Clear";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(clearButton));
            button.Click();
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(false, page.Instance.FilterData);
        }

        #endregion FilterData
    
        #region SelectProduct

        /// <summary>
        /// Test for getting content of selected product
        /// </summary>
        [Test]
        public void SelectProduct_Valid_ID_Should_Return_Content()
        {
            // Act
            var id = "1";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("t-challa"));
        }

        #endregion SelectProduct

        #region Comment
        /// <summary>
        /// Test for checking if comment is present
        /// </summary>

        [Test]
        public void Select_comment_should_return_true()
        {
            // Act
            var page = RenderComponent<ProductList>();
            var button = page.Find("#AddComment");
            button.Click();
            var pageMarkup = page.Markup;
            var temp = page.Instance.NewComment;


            page.Instance.NewComment = false;
            page.SetParametersAndRender();
            // Assert
            Assert.AreEqual(true, temp);

        }
        #endregion Comment
    }
}