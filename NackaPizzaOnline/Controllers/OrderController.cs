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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace NackaPizzaOnline.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly OrderService _orderService;
        private readonly CartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        const string SessionCartId = "CartId";

        public OrderController(ApplicationDbContext context, OrderService orderService, CartService cartService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _orderService = orderService;
            _cartService = cartService;
            _userManager = userManager;
        }

        [HttpPost]
        public ActionResult CheckOut(string cartId)
        {
            var order = _orderService.CreateOrderFromCart(cartId, HttpContext.User);
            if (User.Identity.IsAuthenticated)
            {
               var user = _context.Users.FirstOrDefault(u => u.Id == order.UserId);
                ViewBag.UserAddress = user.Address;
                ViewBag.UserZipCode = user.ZipCode;
                ViewBag.UserCity = user.City;
            }            
            return View(order);
        }

        [HttpPost]
        public ActionResult SendOffToBake(Order order)
        {
            if (_orderService.SaveOrderWhitAllData(order))
            {
                _cartService.RemoveCart(HttpContext.Session.GetString("CartId"));
                HttpContext.Session.Remove(SessionCartId);
            }

            if (!order.Anonymous)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == order.UserId);
                ViewBag.UserAddress = user.Address;
                ViewBag.UserZipCode = user.ZipCode;
                ViewBag.UserCity = user.City;

            }
            return View("BakeConfirmed", order);
        }
    }
}