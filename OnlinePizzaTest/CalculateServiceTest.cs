using System;
using NackaPizzaOnline.Models;
using NackaPizzaOnline.Services;
using Xunit;
using System.Collections.Generic;

namespace OnlinePizzaTest
{
    public class CalculateServiceTest
    {
        [Fact]
        public void CanCountCorrectPriceFromCartItems()
        {
            var cart = GetCart();
            var cartItem = GetCartItem();
            var dish = GetDish();

            var service = new CalculateService();
            var countingCartItem = service.CountingTotalSumOnCartItem(cartItem.CartItemId, cart, dish);

            Assert.Equal(45, countingCartItem);
        }

        [Fact]
        public void CanCalculateCartTotalSum()
        {
            var cart = GetCart();

            var service = new CalculateService();
            var calculateCart = service.CountingTotalToCartInView(cart);

            Assert.Equal(40, calculateCart);
        }

        public Cart GetCart()
        {
            var a = new Ingredient { IngredientId = 1, PriceIfExtra = 5, Name = "Morot" };
            var b = new Ingredient { IngredientId = 2, PriceIfExtra = 5, Name = "Sallad" };
            var c = new Ingredient { IngredientId = 3, PriceIfExtra = 5, Name = "Gurka" };

            var sallad = new Category { CategoryId = 1, Name = "Sallad" };

            var dish = new Dish { Name = "Cesar sallad", Category = sallad, DishId = 1, Price = 40 };

            var aDish = new DishIngredient { Dish = dish, DishId = dish.DishId, Ingredient = a, IngredientId = a.IngredientId };
            var bDish = new DishIngredient { Dish = dish, DishId = dish.DishId, Ingredient = b, IngredientId = b.IngredientId };

            var cartItem = new CartItem { CartId = "cart", CartItemId = 1, DishId = dish.DishId, Sum = dish.Price };

            var aCartItemIngredient = new CartItemIngredients { CartItem = cartItem, CartItemId = 1, Ingredient = a, IngredientId = a.IngredientId };
            var bCartItemIngredient = new CartItemIngredients { CartItem = cartItem, CartItemId = 1, Ingredient = b, IngredientId = b.IngredientId };
            var cCartItemIngredient = new CartItemIngredients { CartItem = cartItem, CartItemId = 1, Ingredient = c, IngredientId = c.IngredientId };

            var cartItemList = new List<CartItem>();
            cartItemList.Add(cartItem);

            var cart = new Cart { CartId = "cart", CartItems = cartItemList };

            return cart;
        }

        public CartItem GetCartItem()
        {
            var sallad = new Category { CategoryId = 1, Name = "Sallad" };
            var dish = new Dish { Name = "Cesar sallad", Category = sallad, DishId = 1, Price = 40 };
            var cartItem = new CartItem { CartId = "cart", CartItemId = 1, DishId = dish.DishId };

            return cartItem;
        }

        public Dish GetDish()
        {
            var sallad = new Category { CategoryId = 1, Name = "Sallad" };
            var dish = new Dish { Name = "Cesar sallad", Category = sallad, DishId = 1, Price = 40 };

            return dish;
        }
    }
}
