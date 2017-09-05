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

namespace NackaPizzaOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        public CartController(ApplicationDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        [HttpGet]
        public void AddDishToCart(int id, List<int> listOfIngredients)
        {

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
