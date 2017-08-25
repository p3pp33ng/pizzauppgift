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
                Dishes = _context.Dishes.Include("DishIngredients").ToList(),
                Ingredients = _context.Ingredients.ToList()
            };
            return View(model);
        }

        //public Dish GetDishInfoForModal(int id)
        //{
        //    var dish = _context.Dishes.Single(d => d.DishId == id);
        //    return Json(new {  });
        //}

        #region Cart
        public PartialViewResult AddToCart()
        {
            return PartialView();
        }

        public void AddCart()
        {

        }
        #endregion
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
