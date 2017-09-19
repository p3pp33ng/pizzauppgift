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
using NackaPizzaOnline.Services;
using Microsoft.AspNetCore.Http;

namespace NackaPizzaOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DishIngredientService _dishIngredientService;
        private readonly CartService _cartService;

        public HomeController(ApplicationDbContext context, DishIngredientService dishIngredientService, CartService cartService)
        {
            _context = context;
            _dishIngredientService = dishIngredientService;
            _cartService = cartService;
        }
        public IActionResult Index()
        {
            var model = new MenyViewModel
            {
                Categories = _context.Categories.ToList(),
                Dishes = _context.Dishes.Include(d => d.DishIngredients).ToList(),
                Ingredients = _context.Ingredients.ToList()
            };
            var session = HttpContext.Session.GetString("CartId");
            if (session != null)
            {
                GetCartIfExsists(session);
            }
            return View(model);
        }

        public PartialViewResult GetDishInfoForModal(int id)
        {
            var viewModel = _dishIngredientService.BuildCustomizeViewModel(id);

            return PartialView("_CustomizeView", viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult GetCartIfExsists(string cartId)
        {
            var cart = _cartService.GetCart(cartId);
            return PartialView("_CartView",cart);
        }
    }
}
