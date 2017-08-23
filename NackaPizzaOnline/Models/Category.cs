using System.ComponentModel.DataAnnotations;

namespace NackaPizzaOnline.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Display (Name = "Namn")]
        public string Name { get; set; }
    }
}