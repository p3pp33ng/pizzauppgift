﻿using System;
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
        public Dish Dish { get; set; }
        public List<CartItemIngredients> CartItemIngredients { get; set; } = new List<CartItemIngredients>();
    }
}
