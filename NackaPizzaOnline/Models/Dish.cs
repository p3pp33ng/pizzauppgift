using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        [Display(Name = "Namn")]
        public string Name { get; set; }
        [Display(Name = "Pris")]
        public int Price { get; set; }
        public byte[] Picture { get; set; }//TODO Fixa så att man kan spara bilder till dom olika maträtterna.
        [Display(Name="Ingredienser")]
        public List<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>();
        [Display(Name="Kategori")]
        public Category Category { get; set; }
    }
}
