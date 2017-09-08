﻿using System;
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
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private ISession _session => _httpContextAccessor.HttpContext.Session;
        //HttpContextAccessor httpContextAccessor
        //public const string CartSessionKey = "CartId";

        public CartController(ApplicationDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
            //_httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public ActionResult AddDishToCart(int id, string stringOfIngredients)
        {
            //"[\"1\",\"2\",\"3\"]"
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
            //var session = _session.GetString(CartSessionKey);
            //if (session != null)
            //{
                //cart = _context.Carts.First(c => c.CartId == session);

                //cart = _cartService.AddCartItem(cart.CartId, id, listOfIngredients);
                //_session.SetString(CartSessionKey, cart.CartId);
            //}
            //else
            //{
                ////TODO skapa en ny cart och lägg till ett cartitem
                //var newCart = _cartService.CreateCart();
                //cart.CartId = newCart.Result.CartId;
                //cart = _cartService.AddCartItem(cart.CartId, id, listOfIngredients);
                //_session.SetString(CartSessionKey, cart.CartId);
            //}

            return PartialView("_CartView", cart);
        }
    }
}

//        // GET: Cart
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.Carts.ToListAsync());
//        }

//        // GET: Cart/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var cart = await _context.Carts
//                .SingleOrDefaultAsync(m => m.CartId == id);
//            if (cart == null)
//            {
//                return NotFound();
//            }

//            return View(cart);
//        }

//        // GET: Cart/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Cart/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("CartId,Sum,UserId")] Cart cart)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(cart);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(cart);
//        }

//        // GET: Cart/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var cart = await _context.Carts.SingleOrDefaultAsync(m => m.CartId == id);
//            if (cart == null)
//            {
//                return NotFound();
//            }
//            return View(cart);
//        }

//        // POST: Cart/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("CartId,Sum,UserId")] Cart cart)
//        {
//            if (id != cart.CartId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(cart);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!CartExists(cart.CartId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(cart);
//        }

//        // GET: Cart/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var cart = await _context.Carts
//                .SingleOrDefaultAsync(m => m.CartId == id);
//            if (cart == null)
//            {
//                return NotFound();
//            }

//            return View(cart);
//        }

//        // POST: Cart/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var cart = await _context.Carts.SingleOrDefaultAsync(m => m.CartId == id);
//            _context.Carts.Remove(cart);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool CartExists(int id)
//        {
//            return _context.Carts.Any(e => e.CartId == id);
//        }
//    }
//}
