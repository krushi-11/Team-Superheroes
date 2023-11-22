using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.Enums
{
    [TestFixture]
    public class ProductTypeEnumTests
    {
        /// <summary>
        /// Enum Display names should be correct
        /// </summary>
        [Test]
        public void ProductTypeEnum_Display_Names_Should_Be_Correct()
        {
            // Arrange
            var undefined = ProductTypeEnum.Undefined;
            var shirts = ProductTypeEnum.Shirts;
            var cups = ProductTypeEnum.Cups;
            var caps = ProductTypeEnum.Caps;

            // Act
            var undefinedDisplayName = GetEnumDisplayName(undefined);
            var shirtsDisplayName = GetEnumDisplayName(shirts);
            var cupsDisplayName = GetEnumDisplayName(cups);
            var capsDisplayName = GetEnumDisplayName(caps);

            // Assert
            Assert.AreEqual("Undefined", undefinedDisplayName);
            Assert.AreEqual("Shirts", shirtsDisplayName);
            Assert.AreEqual("Cups", cupsDisplayName);
            Assert.AreEqual("Caps", capsDisplayName);
        }

        private string GetEnumDisplayName(ProductTypeEnum value)
        {
            // Use reflection to get the Display Name attribute value
            var fieldInfo = value.GetType().GetField(value.ToString());
            var displayAttribute = (DisplayAttribute)fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false)[0];
            return displayAttribute.Name;
        }
    }
}
