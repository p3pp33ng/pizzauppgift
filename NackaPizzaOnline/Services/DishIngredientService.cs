using Microsoft.EntityFrameworkCore;
using NackaPizzaOnline.Data;
using NackaPizzaOnline.Models;
using NackaPizzaOnline.Models.EditViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Services
{
    public class DishIngredientService
    {
        private ApplicationDbContext _context;

        public DishIngredientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool DeleteDishIngredientsOnOldDish(int id)
        {
            var result = false;
            try
            {
                var oldDishIngredients = _context.DishIngredients.Where(di => di.DishId == id).ToList();
                _context.RemoveRange(oldDishIngredients);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            result = true;
            return result;
        }
        public Dish GetEditedDish(EditViewModel editDish)
        {
            var dish = _context.Dishes
                            .Include(d => d.DishIngredients)
                            .ThenInclude(di => di.Ingredient)
                            .Include(d => d.Category)
                            .FirstOrDefault(d => d.DishId == editDish.Dish.DishId);

            dish.Name = editDish.Dish.Name;
            dish.Price = editDish.Dish.Price;
            dish.Category = _context.Categories.FirstOrDefault(c => c.CategoryId == editDish.Dish.Category.CategoryId);

            var ingredients = _context.Ingredients.ToList();
            foreach (var item in editDish.Ingredients)
            {
                if (item.Selected)
                {
                    var newDishIngredient = new DishIngredient
                    {
                        Dish = dish,
                        DishId = dish.DishId,
                        Ingredient = ingredients.FirstOrDefault(i => i.IngredientId == int.Parse(item.Value)),
                        IngredientId = int.Parse(item.Value)
                    };
                    dish.DishIngredients.Add(newDishIngredient);
                }
            }
            return dish;
        }
    }
}
