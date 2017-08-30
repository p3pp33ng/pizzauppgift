using System.ComponentModel.DataAnnotations;

namespace NackaPizzaOnline.Models
{
    public class DishIngredient
    {
        public int DishId { get; set; }
        [Display(Name = "Maträtt")]
        public Dish Dish { get; set; }
        public int IngredientId { get; set; }
        [Display(Name = "Ingrediens")]
        public Ingredient Ingredient { get; set; }
    }
}