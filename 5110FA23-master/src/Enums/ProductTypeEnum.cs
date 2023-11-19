using System.ComponentModel.DataAnnotations;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Defines an enum for the different types
    /// </summary>
    public enum ProductTypeEnum
    {
        // Adding categories for ProductType list displayed on update page for admin to choose from
        [Display(Name = "Undefined")] Undefined = 0,
        [Display(Name = "Shirts")] Scary = 1,
        [Display(Name = "Cups")] Kids = 2,
        [Display(Name = "Caps")] Princess = 3,

    }
}