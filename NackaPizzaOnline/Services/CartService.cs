using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NackaPizzaOnline.Data;
using NackaPizzaOnline.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace NackaPizzaOnline.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Cart CreateCart(ClaimsPrincipal user)
        {
            //Skapa en cart
            var cart = new Cart();

            if (user.Identity.Name != null)
            {
                cart.CartId = user.Identity.Name;
                _context.Carts.Add(cart);
            }
            else
            {
                Guid tempCartId = Guid.NewGuid();
                cart.CartId = tempCartId.ToString();
                _context.Carts.Add(cart);
            }
            _context.SaveChanges();
            return cart;
        }

        public Cart AddCartItem(string cartId, int dishId, List<int> listOfIngredients)
        {
            //lägg till cartitem i cart. Spara cart   
            var cart = _context.Carts.Include(c=>c.CartItems).ThenInclude(ci=>ci.Ingredients).First(c => c.CartId == cartId);
            var dish = _context.Dishes.First(d => d.DishId == dishId);
            var ingredients = _context.Ingredients.ToList();
            var cartItem = new CartItem
            {
                CartId = cartId,
                DishId = dishId
            };

            foreach (var ingredient in ingredients)
            {
                if (listOfIngredients.Contains(ingredient.IngredientId))
                {
                    cartItem.Ingredients.Add(ingredient);
                }
            }
            _context.CartItems.Add(cartItem);           
            _context.Carts.Update(cart);
            _context.SaveChanges();

            CountingTotalSum(cartItem.CartItemId);

            return cart;
        }

        public bool RemoveCartItem(string cartId, int cartItemId)
        {
            //Ta bort en vara ur cart.
            return false;
        }

        public void RemoveCart(string cartid)
        {

        }

        public void CountingTotalSum(int cartItemId)
        {
            var result = 0;
            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
            var ingredients = _context.Ingredients.ToList();
            var dish = _context.Dishes.Include(d=>d.DishIngredients).ThenInclude(di=>di.Ingredient).FirstOrDefault(d => d.DishId == cartItem.DishId);
            var orginalIngredients = _context.DishIngredients.Include(di => di.Ingredient).Where(i=>i.DishId == dish.DishId).Select(di=>di.Ingredient).ToList();

            result += dish.Price;

            foreach (var item in cartItem.Ingredients)
            {
                if (!orginalIngredients.Contains(item))
                {
                    result += item.PriceIfExtra;
                }
            }
            cartItem.Sum = result;
            _context.CartItems.Update(cartItem);
            _context.SaveChanges();
        }
    }
}
