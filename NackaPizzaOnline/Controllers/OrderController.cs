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

        [HttpGet]
        public ActionResult Checkout()
        {
            var cartId = HttpContext.Session.GetString("CartId");
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
        public ActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
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
                if (order.PayMethod == PayMethods.CreditCard)
                {
                    ViewBag.OrderId = order.OrderId;
                    var creditCardModel = new CreditCardModel();
                    return View("CreditCardView", creditCardModel);
                }
                return View("BakeConfirmed", order);
            }            
            var orderTwo = _context.Orders.Include(o => o.CartItems).First(o=>o.OrderId == order.OrderId);
            return View(orderTwo);
        }

        [HttpPost]
        public ActionResult CheckCreditCard(CreditCardModel model, int orderId)
        {
            if (ModelState.IsValid)
            {
                var order = _context.Orders.FirstOrDefault(o=>o.OrderId == orderId);
                if (!order.Anonymous)
                {
                    var user = _context.Users.FirstOrDefault(u => u.Id == order.UserId);
                    ViewBag.UserAddress = user.Address;
                    ViewBag.UserZipCode = user.ZipCode;
                    ViewBag.UserCity = user.City;
                }
                return View("BakeConfirmed", order);
            }
            return View("CreditCardView", model);
        }

        [HttpPost]
        public ActionResult GetAllOrders(string id)
        {
            return View(_orderService.GetAllOrdersForUser(id));
        }

        public ActionResult AllClear(int orderId)
        {
            var order = _context.Orders.Single(o=>o.OrderId == orderId);
            if (User.Identity.IsAuthenticated)
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