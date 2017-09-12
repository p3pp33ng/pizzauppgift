using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NackaPizzaOnline.Data;
using NackaPizzaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Order CreateOrderFromCart(string cartId, ClaimsPrincipal user = null)
        {
            //TODO Test if this check out.
            var order = new Order
            {
                Paid = false,
                PayMethod = PayMethods.NotStillPayed,
                Anonymous = true ? user == null : false,
                TotalAmount = _context.Carts.FirstOrDefault(c => c.CartId == cartId).Sum,
                UserId = _userManager.GetUserId(user) ?? "",
                CartItems = _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.CartItemIngredients).FirstOrDefault(c => c.CartId == cartId).CartItems
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }
    }
}
