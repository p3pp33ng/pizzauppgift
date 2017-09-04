using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NackaPizzaOnline.Models;
using NackaPizzaOnline.Models.HomeViewModels;
using NackaPizzaOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NackaPizzaOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new MenyViewModel
            {
                Categories = _context.Categories.ToList(),
                Dishes = _context.Dishes.Include(d => d.DishIngredients).ToList(),
                Ingredients = _context.Ingredients.ToList()
            };
            return View(model);
        }

        public PartialViewResult GetDishInfoForModal(int id)
        {
            var dish = _context.Dishes
                .Single(d => d.DishId == id);
            var dishIngredients = _context.DishIngredients
                .Include(di => di.Ingredient)
                .Where(di => di.DishId == id)
                .OrderBy(di => di.IngredientId)
                .ToList();
            var allIngredients = _context.Ingredients
                .OrderBy(i => i.IngredientId)
                .ToList();

            var list = new List<SelectListItem>();
            foreach (var ingredient in allIngredients)
            {                
                if (dishIngredients.Exists(i => i.IngredientId == ingredient.IngredientId))
                {
                    var ingredientChecked = new SelectListItem
                    {
                        Value = ingredient.IngredientId.ToString(),
                        Selected = true,
                        Text = ingredient.Name
                    };
                    list.Add(ingredientChecked);
                }
                else
                {
                    var ingredientNotChecked = new SelectListItem
                    {
                        Value = ingredient.IngredientId.ToString(),
                        Text = ingredient.Name
                    };
                    list.Add(ingredientNotChecked);
                }
            }

            var viewModel = new CustomizeViewModel
            {
                Dish = dish,
                Ingredients = list
            };
            return PartialView("_CustomizeView", viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
