using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public string CartId { get; set; }
        public int DishId { get; set; }
        public int Sum { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
