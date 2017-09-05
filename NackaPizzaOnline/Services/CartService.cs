using NackaPizzaOnline.Data;
using NackaPizzaOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsCartCreated(int? id)
        {
            //Kolla om det finns en cart
            var result = false;

            return result;
        }

        public Cart CreateCart()
        {
            //Skapa en cart
            return new Cart();
        }

        public CartItem AddCartItem(int cartId, int dishId, List<int> listOfIngredients)
        {
            //lägg till cartitem i cart.
            return new CartItem();
        }

        public bool RemoveCartItem(int cartId, int cartItemId)
        {
            //Ta bort en vara ur cart.
            return false;
        }
    }
}
