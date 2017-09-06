using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models
{
    public class Cart
    {
        public string CartId { get; set; }
        [Display(Name="Kundkorg")]
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        [Display(Name ="Summa")]
        public int Sum { get; set; }
        public string UserId { get; set; }
    }
}
