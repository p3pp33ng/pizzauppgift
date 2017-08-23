using System.Collections.Generic;

namespace NackaPizzaOnline.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public int PriceIfExtra { get; set; }
        public List<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>();
    }
}