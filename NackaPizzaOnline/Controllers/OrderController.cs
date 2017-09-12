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
using Microsoft.AspNetCore.Http;

namespace NackaPizzaOnline.Controllers
{    
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly OrderService _orderService;
        private readonly CartService _cartService;

        const string SessionCartId = "CartId";

        public OrderController(ApplicationDbContext context, OrderService orderService, CartService cartService)
        {
            _context = context;
            _orderService = orderService;
            _cartService = cartService;
        }

        [HttpPost]
        public ActionResult CheckOut(string cartId)
        {
           var order = _orderService.CreateOrderFromCart(cartId, HttpContext.User);
            return View(order);
        }

        [HttpPost]
        public ActionResult SendOffToBake(Order order)
        {
            _cartService.RemoveCart(HttpContext.Session.GetString("CartId"));
            HttpContext.Session.Remove(SessionCartId);
            return View("BakeConfirmed");
        }
    }
}

//        // GET: Order
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.Orders.ToListAsync());
//        }

//        // GET: Order/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var order = await _context.Orders
//                .SingleOrDefaultAsync(m => m.OrderId == id);
//            if (order == null)
//            {
//                return NotFound();
//            }

//            return View(order);
//        }

//        // GET: Order/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Order/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("OrderId,TotalAmount,Paid,PayMethod")] Order order)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(order);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(order);
//        }

//        // GET: Order/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
//            if (order == null)
//            {
//                return NotFound();
//            }
//            return View(order);
//        }

//        // POST: Order/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("OrderId,TotalAmount,Paid,PayMethod")] Order order)
//        {
//            if (id != order.OrderId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(order);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!OrderExists(order.OrderId))
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
//            return View(order);
//        }

//        // GET: Order/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var order = await _context.Orders
//                .SingleOrDefaultAsync(m => m.OrderId == id);
//            if (order == null)
//            {
//                return NotFound();
//            }

//            return View(order);
//        }

//        // POST: Order/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
//            _context.Orders.Remove(order);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool OrderExists(int id)
//        {
//            return _context.Orders.Any(e => e.OrderId == id);
//        }
//    }
//}
