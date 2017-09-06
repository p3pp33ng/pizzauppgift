using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public CartService(ApplicationDbContext context, HttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        //public bool IsCartCreated(int? id)
        //{
        //    //Kolla om det finns en cart
        //    var result = false;
        //    return result;
        //}

        public Cart CreateCart()
        {
            //Skapa en cart
            var cart = new Cart();
            //Todo om man är inloggad får cart ditt namn eller liknade som id om inte så bygger du en GUID om gör att den blir personlig
            
            //if (session != null)
            //{
            //    cart.CartId = session;
            //}
            //else
            //{

            //}
            return cart;
        }

        public Cart AddCartItem(string cartId, int dishId, List<int> listOfIngredients)
        {
            //lägg till cartitem i cart. Spara cart       
            return new Cart();
        }

        public bool RemoveCartItem(string cartId, int cartItemId)
        {
            //Ta bort en vara ur cart.
            return false;
        }
    }
}
