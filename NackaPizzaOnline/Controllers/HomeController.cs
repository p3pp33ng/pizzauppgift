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

            foreach (var ingredient in allIngredients)
            {

                var item = dishIngredients.Exists(i => i.IngredientId == ingredient.IngredientId);

                if (item)
                {
                    ingredient.IsChecked = true;
                }
                else
                {
                    ingredient.IsChecked = false;
                }
            }

            var viewModel = new CustomizeViewModel
            {
                Dish = dish,
                Ingredients = allIngredients
            };

            return PartialView("_CustomizeView", viewModel);
        }               

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
