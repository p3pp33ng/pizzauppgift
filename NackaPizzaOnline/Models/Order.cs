using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models
{
    public enum PayMethods
    {
        CreditCard,
        Invoice,
        Paypal,
        PayOnArrival,
        NotStillPayed
    }
    public class Order
    {
        public int OrderId { get; set; }
        [Display(Name = "Varor")]
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        [Display(Name = "Summa")]
        public int TotalAmount { get; set; }
        public string UserId { get; set; }
        public bool Anonymous { get; set; }
        public bool Paid { get; set; }
        [Display(Name = "Betalmetod")]
        public PayMethods PayMethod { get; set; }
        [Display(Name = "Adress")]
        public string AnonymousAddress { get; set; }
        [Display(Name = "Postnummer")]
        public string AnonymousZipCode { get; set; }
        [Display(Name = "Stad")]
        public string AnonymousCity { get; set; }
    }
}
