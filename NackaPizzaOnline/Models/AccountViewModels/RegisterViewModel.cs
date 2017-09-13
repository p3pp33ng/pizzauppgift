using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        [Compare("Password", ErrorMessage = "Dom dåda lösenorden är inte lika.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100,ErrorMessage ="{0} måste vara längre än {2} och max {1}.", MinimumLength = 5)]
        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Required]
        [StringLength(5,ErrorMessage ="{0} måste vara max {1} siffror.")]
        [Display(Name = "Postnummer")]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="{0} måste vara max {1} bokstäver.")]
        [Display(Name = "Stad")]
        public string City { get; set; }
    }
}
