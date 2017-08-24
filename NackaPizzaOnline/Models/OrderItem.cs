using System.Collections.Generic;

namespace NackaPizzaOnline.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public Dish Dish { get; set; }
        public int Price { get; set; }
    }
}