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
        public void TotalForCartItem()
        {
            // Arrrange
            var cartItem = GetCartItem();            
            var calculateService = new CalculateService();
            // Act
            var totalForCartItem = calculateService.TotalForCartItem(cartItem);
            // Assert
            Assert.Equal(45, totalForCartItem);
        }

        [Fact]
        public void TotalForCart()
        {
            // Arrange
            var cart = GetCart();
            var service = new CalculateService();
            // Act
            var calculateCart = service.TotalForCart(cart);
            // Assert
            Assert.Equal(55, calculateCart);
        }

        public Cart GetCart()
        {
            var a = new Ingredient { IngredientId = 1, PriceIfExtra = 5, Name = "Morot" };
            var b = new Ingredient { IngredientId = 2, PriceIfExtra = 5, Name = "Sallad" };
            var c = new Ingredient { IngredientId = 3, PriceIfExtra = 5, Name = "Gurka" };

            var sallad = new Category { CategoryId = 1, Name = "Sallad" };

            var dish = new Dish { Name = "Cesar sallad", Category = sallad, DishId = 1, Price = 40, DishIngredients=new List<DishIngredient>() };
            
            var cartItem = new CartItem { CartId = "cart", CartItemId = 1, DishId = dish.DishId, Dish = dish };
            var cartItemList = new List<CartItem>();
            cartItemList.Add(cartItem);

            var aCartItemIngredient = new CartItemIngredients { CartItem = cartItem, CartItemId = 1, Ingredient = a, IngredientId = a.IngredientId };
            var bCartItemIngredient = new CartItemIngredients { CartItem = cartItem, CartItemId = 1, Ingredient = b, IngredientId = b.IngredientId };
            var cCartItemIngredient = new CartItemIngredients { CartItem = cartItem, CartItemId = 1, Ingredient = c, IngredientId = c.IngredientId };

            cartItem.CartItemIngredients.Add(aCartItemIngredient);
            cartItem.CartItemIngredients.Add(bCartItemIngredient);
            cartItem.CartItemIngredients.Add(cCartItemIngredient);           

            var cart = new Cart { CartId = "cart", CartItems = cartItemList };

            return cart;
        }

        public CartItem GetCartItem()
        {
            var dish = GetDish();

            var cartItem = new CartItem { CartId = "cart", CartItemId = 1, DishId = dish.DishId, Dish = dish };

            var a = new Ingredient { IngredientId = 1, PriceIfExtra = 5, Name = "Morot" };
            var b = new Ingredient { IngredientId = 2, PriceIfExtra = 5, Name = "Sallad" };
            var c = new Ingredient { IngredientId = 3, PriceIfExtra = 5, Name = "Gurka" };

            var aCartItemIngredient = new CartItemIngredients { CartItem = cartItem, CartItemId = 1, Ingredient = a, IngredientId = a.IngredientId };
            var bCartItemIngredient = new CartItemIngredients { CartItem = cartItem, CartItemId = 1, Ingredient = b, IngredientId = b.IngredientId };
            var cCartItemIngredient = new CartItemIngredients { CartItem = cartItem, CartItemId = 1, Ingredient = c, IngredientId = c.IngredientId };
            cartItem.CartItemIngredients.Add(aCartItemIngredient);
            cartItem.CartItemIngredients.Add(bCartItemIngredient);
            cartItem.CartItemIngredients.Add(cCartItemIngredient);
            return cartItem;
        }

        public Dish GetDish()
        {
            var a = new Ingredient { IngredientId = 1, PriceIfExtra = 5, Name = "Morot" };
            var b = new Ingredient { IngredientId = 2, PriceIfExtra = 5, Name = "Sallad" };

            var sallad = new Category { CategoryId = 1, Name = "Sallad" };

            var dish = new Dish { Name = "Cesar sallad", Category = sallad, DishId = 1, Price = 40 };

            var aDish = new DishIngredient { Dish = dish, DishId = dish.DishId, Ingredient = a, IngredientId = a.IngredientId };
            var bDish = new DishIngredient { Dish = dish, DishId = dish.DishId, Ingredient = b, IngredientId = b.IngredientId };
            var list = new List<DishIngredient>();
            list.Add(aDish);
            list.Add(bDish);
            dish.DishIngredients.AddRange(list);
            return dish;
        }
    }
}
