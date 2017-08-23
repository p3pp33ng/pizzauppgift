﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public byte[] Picture { get; set; }//Todo Fixa så att man kan spara bilder till dom olika maträtterna.
        public List<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>();
        public int CategoryId { get; set; }
    }
}
