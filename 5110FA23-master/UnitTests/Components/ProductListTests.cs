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

        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increament_Count_And_Check_Star()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_scarlet-witch";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Get the markup of the page post the click action
            var buttonMarkup = page.Markup;

            // Get the star Buttons
            var starButtonList = page.FindAll("span");

            // Get the vote Count
            // get the vote count, the List should have 7 element, element 2 is string for count
            var preVoteCountSpan = starButtonList[1];
            var preVoteCountString = preVoteCountSpan.OuterHtml;

            // Get the First star item from the list, it should not be checked
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-smile"));

            // Save the html for it to compare after the click 
            var preStarChange = starButton.OuterHtml;

            //Act 

            // Click the star button
            starButton.Click();

            //Get the markup to use for the assert
            buttonMarkup = page.Markup;

            // Get the Star Buttons
            starButtonList = page.FindAll("span");

            // Get the Vote Count, the List should 7 elements, element 2 is the string for the count
            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = postVoteCountSpan.OuterHtml;

            // Get the last Stared item from the list
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-smile checked"));

            // Save the html for it to compare after the click 
            var postStarChange = starButton.OuterHtml;

            // Assert

            // Configure that the record had no votes to start, and 1 vote after
            Assert.AreEqual(true, preVoteCountString.Contains("Like our Shirt"));
            Assert.AreEqual(true, postVoteCountString.Contains("1 Vote"));
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }
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