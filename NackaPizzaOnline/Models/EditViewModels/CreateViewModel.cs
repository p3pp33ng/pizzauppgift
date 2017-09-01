using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models.EditViewModels
{
    public class CreateViewModel
    {
        public Dish Dish { get; set; }
        [Display(Name = "Ingredienser")]
        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
