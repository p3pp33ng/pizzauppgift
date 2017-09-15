using NackaPizzaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Services
{
    delegate int Cool(CartItem cartItem);
    public class CalculateService
    {
        public int TotalForCart(Cart cart)
        {
            var result = 0;
            cart.CartItems.ForEach(ci => result += TotalForCartItem(ci));
            return result;
        }

        public int TotalForCartItem(CartItem cartItem)
        {
            var result = cartItem.Dish.Price +
                cartItem.CartItemIngredients
                    .Where(cii => !cartItem.Dish.DishIngredients
                    .Any(di => di.IngredientId == cii.IngredientId))
                    .Sum(cii => cii.Ingredient.PriceIfExtra);
            return result;
        }
    }
}
