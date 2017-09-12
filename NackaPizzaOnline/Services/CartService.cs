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

        public Cart CreateCart()
        {
            //Skapa en cart
            var cart = new Cart();

            //if (user.Identity.Name != null)
            //{
            //    cart.CartId = user.Identity.Name;
            //    _context.Carts.Add(cart);
            //}
            //else
            //{               
            //}
            Guid tempCartId = Guid.NewGuid();
            cart.CartId = tempCartId.ToString();
            _context.Carts.Add(cart);
            _context.SaveChanges();

            return cart;
        }

        public Cart AddCartItem(string cartId, int dishId, List<int> listOfIngredients)
        {
            //lägg till cartitem i cart. Spara cart   
            var cart = _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.CartItemIngredients).First(c => c.CartId == cartId);
            var dish = _context.Dishes.First(d => d.DishId == dishId);
            var ingredients = _context.Ingredients.ToList();
            var cartItem = new CartItem
            {
                CartId = cartId,
                DishId = dishId
            };
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();

            foreach (var ingredient in ingredients)
            {
                if (listOfIngredients.Contains(ingredient.IngredientId))
                {
                    cartItem.CartItemIngredients.Add(new CartItemIngredients
                    {
                        CartItem = cartItem,
                        CartItemId = cartItem.CartItemId,
                        Ingredient = ingredient,
                        IngredientId = ingredient.IngredientId
                    });
                }
            }
            CountingTotalSumOnCartItem(cartItem.CartItemId);
            _context.CartItems.Update(cartItem);
            cart.Sum += cartItem.Sum;
            _context.Carts.Update(cart);
            _context.SaveChanges();

            return cart;
        }

        public Cart RemoveCartItem(string cartId, int cartItemId)
        {
            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.CartId == cartId);
            cart.CartItems.Remove(_context.CartItems.First(ci => ci.CartItemId == cartItemId));
            _context.Carts.Update(cart);
            _context.SaveChanges();

            return cart;
        }

        public void RemoveCart(string cartid)
        {
            _context.Carts.Remove(_context.Carts.First(c => c.CartId == cartid));
            _context.SaveChanges();
        }

        public void CountingTotalSumOnCartItem(int cartItemId)
        {
            var result = 0;
            var cartItem = _context.CartItems.Include(ci => ci.CartItemIngredients).FirstOrDefault(ci => ci.CartItemId == cartItemId);
            var ingredients = _context.Ingredients.ToList();
            var dish = _context.Dishes.Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient).FirstOrDefault(d => d.DishId == cartItem.DishId);
            var orginalIngredients = _context.DishIngredients.Include(di => di.Ingredient).Where(i => i.DishId == dish.DishId).Select(di => di.Ingredient).ToList();

            result += dish.Price;

            foreach (var item in cartItem.CartItemIngredients)
            {
                if (!orginalIngredients.Contains(item.Ingredient))
                {
                    result += item.Ingredient.PriceIfExtra;
                }
            }
            cartItem.Sum = result;
            _context.CartItems.Update(cartItem);
            _context.SaveChanges();
        }

        public int CountingTotalToCartInView(string cartId)
        {
            var result = 0;
            var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.CartId == cartId);
            foreach (var cartItem in cart.CartItems.ToList())
            {
                result += cartItem.Sum;
            }
            return result;
        }
    }
}
