using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NackaPizzaOnline.Data;
using NackaPizzaOnline.Models;
using NackaPizzaOnline.Services;
using NackaPizzaOnline.Models.HomeViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace NackaPizzaOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        const string SessionCartId = "CartId";

        public CartController(ApplicationDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }
        [HttpGet]
        public ActionResult AddDishToCartAfterCustomizing(int id, string stringOfIngredients)
        {
            var split = Regex.Split(stringOfIngredients, @"\D+");

            var listOfIngredients = new List<int>();
            foreach (var item in split)
            {
                if (int.TryParse(item, out int result))
                {
                    listOfIngredients.Add(int.Parse(item));
                }
            }

            var cart = new Cart();
            var session = HttpContext.Session.GetString(SessionCartId);
            if (session != null)
            {
                cart = _context.Carts.First(c => c.CartId == session);

                cart = _cartService.AddCartItem(cart.CartId, id, listOfIngredients);
                HttpContext.Session.SetString(SessionCartId, cart.CartId);
            }
            else
            {
                var newCart = _cartService.CreateCart(User);
                cart.CartId = newCart.CartId;
                cart = _cartService.AddCartItem(cart.CartId, id, listOfIngredients);
                HttpContext.Session.SetString(SessionCartId, cart.CartId);
            }

            return PartialView("_CartView", cart);
        }

        [HttpGet]
        public ActionResult AddDishWithoutCustomizing(int id)
        {
            //TODO Refaktorera denna metod så att man bara behöver använda den ena metoden.
            var cart = new Cart();
            var dish = _context.Dishes.Include(d=> d.DishIngredients).ThenInclude(i=>i.Ingredient).FirstOrDefault(d=>d.DishId == id);
            var session = HttpContext.Session.GetString(SessionCartId);

            var listOfIngredients = new List<int>();

            foreach (var item in dish.DishIngredients)
            {
                listOfIngredients.Add(item.IngredientId);
            }

            if (session != null)
            {
                cart = _context.Carts.First(c => c.CartId == session);

                cart = _cartService.AddCartItem(cart.CartId, id, listOfIngredients);
                HttpContext.Session.SetString(SessionCartId, cart.CartId);
            }
            else
            {
                var newCart = _cartService.CreateCart(User);
                cart.CartId = newCart.CartId;
                cart = _cartService.AddCartItem(cart.CartId, id, listOfIngredients);
                HttpContext.Session.SetString(SessionCartId, cart.CartId);
            }

            return PartialView("_CartView", cart);
        }
    }
}