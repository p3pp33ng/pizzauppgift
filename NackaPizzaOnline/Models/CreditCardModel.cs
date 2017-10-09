using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models
{
    public class CreditCardModel
    {
        [Required]
        [Display(Name = "Kortnummer")]
        [DataType(DataType.CreditCard)]
        [StringLength(16, ErrorMessage = "{0} måste vara max {1} tecken och minst {2} tecken", MinimumLength = 16)]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Utgångsår")]
        [StringLength(maximumLength : 4, ErrorMessage = "{0} måste ha max {1} tecken och minimum {2} tecken.", MinimumLength = 4)]
        public string ExpireYear { get; set; }

        [Required]
        [Display(Name = "Utgångsmånad")]
        [StringLength(maximumLength: 2, ErrorMessage = "{0} måste ha max {1} tecken och minimum {2} tecken.", MinimumLength = 2)]
        public string ExpireMonth { get; set; }

        [Required]
        [Display(Name = "CVC")]
        [StringLength(maximumLength: 3, ErrorMessage = "{0} måste ha max {1} tecken och minimum {2} tecken.", MinimumLength = 3)]
        public string Cvc { get; set; }
    }
}
