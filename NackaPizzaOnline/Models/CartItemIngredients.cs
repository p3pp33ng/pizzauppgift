using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models
{
    public class CartItemIngredients
    {
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int CartItemId { get; set; }
        public CartItem CartItem { get; set; }
    }
}
