using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models
{
    public enum PayMethods
    {
        PayOnArrival,
        Invoice,
        Paypal,
        CreditCard,
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

        [Required]
        [StringLength(100,ErrorMessage = "{0} får vara max {1} bokstäver.")]
        [Display(Name = "Adress")]
        public string AnonymousAddress { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "{0} måste vara {1} siffror.", MinimumLength = 5)]
        [Display(Name = "Postnummer")]
        public string AnonymousZipCode { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "{0} får inte vara längre än {1} bokstäver.")]
        [Display(Name = "Stad")]
        public string AnonymousCity { get; set; }
    }
}
