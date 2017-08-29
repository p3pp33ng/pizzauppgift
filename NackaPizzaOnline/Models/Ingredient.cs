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
        public bool IsChecked { get; set; }
        public List<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>();
    }
}