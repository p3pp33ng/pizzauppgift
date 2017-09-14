using NackaPizzaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Services
{
    public class CalculateService
    {
        public int CountingTotalToCartInView(Cart cart)
        {
            var result = 0;

            foreach (var cartItem in cart.CartItems)
            {
                result += cartItem.Sum;
            }
            cart.Sum = result;
            return cart.Sum;
        }

        public int CountingTotalSumOnCartItem(int cartItemId, Cart cart, Dish dish)
        {
            var result = 0;
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId).CartItemIngredients.ToList();
            var orginalIngredients = dish.DishIngredients.Where(i => i.DishId == dish.DishId).Select(di => di.Ingredient).ToList();

            result += dish.Price;

            foreach (var item in cartItem)
            {
                if (!orginalIngredients.Contains(item.Ingredient))
                {
                    result += item.Ingredient.PriceIfExtra;
                }
            }

            return result;
        }
    }
}
