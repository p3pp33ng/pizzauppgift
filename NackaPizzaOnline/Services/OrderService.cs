using Microsoft.AspNetCore.Identity;
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
                UserId = _userManager.GetUserId(user) ?? ""
            };

            foreach (var cartItem in _context.CartItems.Where(ci=>ci.CartId == cartId).ToList())
            {
                var orderItem = new OrderItem
                {
                    
                };
                order.OrderItems.Add(orderItem);
            }

            return order;
        }
    }
}
