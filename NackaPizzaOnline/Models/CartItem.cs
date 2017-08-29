using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public List<int> IngredientIds { get; set; } = new List<int>();
        public int DishId { get; set; }
    }
}
