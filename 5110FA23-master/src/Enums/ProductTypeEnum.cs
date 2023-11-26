using System.ComponentModel.DataAnnotations;

namespace SuperHeroes.WebSite.Models
{
    /// <summary>
    /// Defines an enum for the different types
    /// </summary>
    public enum ProductTypeEnum
    {
        // Adding categories for ProductType list displayed on update page for admin to choose from
        [Display(Name = "Undefined")] Undefined = 0,
        [Display(Name = "Shirts")] Shirts = 1,
        [Display(Name = "Cups")] Cups = 2,
        [Display(Name = "Caps")] Caps = 3,

    }
}