using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NackaPizzaOnline.Data;
using NackaPizzaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace NackaPizzaOnline.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private ISession _session => _httpContextAccessor.HttpContext.Session;
        //HttpContextAccessor httpContextAccessor

        public CartService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            //_httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        //public bool IsCartCreated(int? id)
        //{
        //    //Kolla om det finns en cart
        //    var result = false;
        //    return result;
        //}

        public async Task<Cart> CreateCart()
        {
            //Skapa en cart
            var cart = new Cart();
            //TODO om man är inloggad får cart ditt namn eller liknade som id, om inte så bygger du en GUID som gör att den blir personlig.
            //använd httpcontext för att hämta usern som är inne nu.
            //var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            //if (user != null)
            //{
            //    cart.CartId = user.UserName;
            //    _context.Carts.Add(cart);
            //}
            //else
            //{
            //    Guid tempCartId = Guid.NewGuid();
            //    cart.CartId = tempCartId.ToString();
            //    _context.Carts.Add(cart);
            //}
            //_context.SaveChanges();
            return cart;
        }

        public Cart AddCartItem(string cartId, int dishId, List<int> listOfIngredients)
        {
            //lägg till cartitem i cart. Spara cart   
            var cart = _context.Carts.First(c => c.CartId == cartId);
            var dish = _context.Dishes.First(d => d.DishId == dishId);
            var ingredients = _context.Ingredients.ToList();
            var item = new CartItem
            {
                CartId = cartId,
                DishId = dishId
            };
            var list = new List<CartItem>();

            foreach (var ingredient in ingredients)
            {
                if (listOfIngredients.Contains(ingredient.IngredientId))
                {
                    item.Ingredients.Add(ingredient);
                    list.Add(item);
                }

            }
            cart.CartItems.AddRange(list);
            _context.Carts.Update(cart);
            _context.SaveChanges();
            return cart;
        }

        public bool RemoveCartItem(string cartId, int cartItemId)
        {
            //Ta bort en vara ur cart.
            return false;
        }
    }
}
