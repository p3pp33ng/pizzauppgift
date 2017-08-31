using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models.EditViewModels
{
    public class EditViewModel
    {
        public Dish Dish { get; set; }
        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();

        public EditViewModel(Dish dish, List<Ingredient> ingredients)
        {
            Dish = dish;
            
            foreach (var ingredient in ingredients)
            {
                Ingredients.Add(new SelectListItem {
                    Selected = dish.DishIngredients.FirstOrDefault(i => i.IngredientId == ingredient.IngredientId) != null,
                    Text = ingredient.Name,
                    Value=ingredient.IngredientId.ToString()
                } );
            }
        }

        public EditViewModel()
        {

        }
    }
}
