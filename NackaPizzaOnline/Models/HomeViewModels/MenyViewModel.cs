using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models.HomeViewModels
{
    public class MenyViewModel
    {
        public List<Dish> Dishes { get; set; } = new List<Dish>();
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
