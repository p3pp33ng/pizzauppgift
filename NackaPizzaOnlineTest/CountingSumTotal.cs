using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NackaPizzaOnlineTest
{
    [TestClass]
    public class CountingSumTotal
    {
        [TestMethod]
        public void TestCountingSumTotal()
        {

        }
        //public void CountingTotalSumOnCartItem(int cartItemId)
        //{
        //    var result = 0;
        //    var cartItem = _context.CartItems.Include(ci => ci.CartItemIngredients).FirstOrDefault(ci => ci.CartItemId == cartItemId);
        //    var ingredients = _context.Ingredients.ToList();
        //    var dish = _context.Dishes.Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient).FirstOrDefault(d => d.DishId == cartItem.DishId);
        //    var orginalIngredients = _context.DishIngredients.Include(di => di.Ingredient).Where(i => i.DishId == dish.DishId).Select(di => di.Ingredient).ToList();

        //    result += dish.Price;

        //    foreach (var item in cartItem.CartItemIngredients)
        //    {
        //        if (!orginalIngredients.Contains(item.Ingredient))
        //        {
        //            result += item.Ingredient.PriceIfExtra;
        //        }
        //    }
        //    cartItem.Sum = result;
        //    _context.CartItems.Update(cartItem);
        //    _context.SaveChanges();
        //}
    }
}
