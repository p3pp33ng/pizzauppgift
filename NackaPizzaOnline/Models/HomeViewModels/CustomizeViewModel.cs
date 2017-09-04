using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models.HomeViewModels
{
    public class CustomizeViewModel
    {
        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();
        public Dish Dish { get; set; }
    }
}
