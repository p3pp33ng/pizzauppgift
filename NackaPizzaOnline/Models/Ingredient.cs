using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NackaPizzaOnline.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        [Display(Name = "Namn")]
        public string Name { get; set; }
        [Display(Name = "Extrakostnad")]
        public int PriceIfExtra { get; set; }
        public List<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>();
        public List<CartItemIngredients> CartItemIngredients { get; set; } = new List<Models.CartItemIngredients>();
    }
}