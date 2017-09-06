using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models
{
    public enum PayMethods
    {
        CreditCard,
        Invoice,
        Paypal,
        PayOnArrival
    }
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public int TotalAmount { get; set; }
        public ApplicationUser User { get; set; }
        public bool Paid { get; set; }
        public int CartId { get; set; }
        public PayMethods PayMethod { get; set; }
    }
}
