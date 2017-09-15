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
        private readonly CalculateService _calculateService;
        private readonly CartService _cartService;

        public OrderService(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            CalculateService calculateService, CartService cartService)
        {
            _context = context;
            _userManager = userManager;
            _calculateService = calculateService;
            _cartService = cartService;
        }

        public Order CreateOrderFromCart(string cartId, ClaimsPrincipal user = null)
        {            
            var order = new Order
            {
                Paid = false,
                PayMethod = PayMethods.NotStillPayed,
                Anonymous = !user.Identity.IsAuthenticated,
                TotalAmount = _calculateService.TotalForCart(_cartService.GetCart(cartId)),
                UserId = _userManager.GetUserId(user) ?? "",
                CartItems = _context.Carts.Include(c => c.CartItems)
                    .ThenInclude(ci => ci.CartItemIngredients)
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Dish)
                    .ThenInclude(d => d.DishIngredients)
                    .FirstOrDefault(c => c.CartId == cartId).CartItems
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public bool SaveOrderWhitAllData(Order order)
        {         
            //TODO spara ner all data som kommit in via order. För att senare kunna visa det i applicationUser.
            try
            {
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }
    }
}
