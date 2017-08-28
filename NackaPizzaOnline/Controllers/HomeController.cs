﻿using System;
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
        [HttpGet]
        public JsonResult GetDishInfoForModal(int id)
        {
            var dishName = _context.Dishes
                .Single(d => d.DishId == id).Name;
            var dishIngredients = _context.DishIngredients
                .Include("Ingredient")
                .Where(di => di.DishId == id)
                .OrderBy(di => di.IngredientId)
                .ToList();
            var allIngredients = _context.Ingredients
                .OrderBy(i=>i.IngredientId)
                .ToList();
            

            var otherIngredients = new Dictionary<int, string>();
            var dishAlreadyHaveIngredients = new Dictionary<int,string>();
            //var remainingIngredients = new Dictionary<int, string>();
            foreach (var curretIngredient in dishIngredients)
            {
                dishAlreadyHaveIngredients.Add(curretIngredient.Ingredient.IngredientId,curretIngredient.Ingredient.Name);
            }
            foreach (var otherIngredient in allIngredients)
            {
                otherIngredients.Add(otherIngredient.IngredientId, otherIngredient.Name);
            }
           var remainingIngredients = otherIngredients.Except(dishAlreadyHaveIngredients);
            var json = new { dishName, dishAlreadyHaveIngredients, remainingIngredients };
            return Json(json);
        }

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
