using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NackaPizzaOnline.Data;
using NackaPizzaOnline.Models;
using NackaPizzaOnline.Models.EditViewModels;
using NackaPizzaOnline.Models.HomeViewModels;
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

        public string GetNameOfDish(int id)
        {            
            return _context.Dishes.First(d=>d.DishId == id).Name;
        }

        public string WriteOutIngredients(string cartId, int cartItemId)
        {
            var cart = _context.Carts.Include(c => c.CartItems).ThenInclude(ci=>ci.CartItemIngredients).FirstOrDefault(c => c.CartId == cartId);
            var cartItem = cart.CartItems.FirstOrDefault(ci=>ci.CartItemId == cartItemId);
            var result = "";

            for(var i = 0; i < cartItem.CartItemIngredients.Count; ++i)
                    {
                if (i != cartItem.CartItemIngredients.Count - 1)
                {
                    result += cartItem.CartItemIngredients[i].Ingredient.Name + ", ";
                }
                else
                {
                    if (cartItem.CartItemIngredients.Count == 1)
                    {
                        result += cartItem.CartItemIngredients[i].Ingredient.Name;
                    }
                    else
                    {
                        result += "och " + cartItem.CartItemIngredients[i].Ingredient.Name + ".";
                    }
                }
            }
            return result;
        }

        public bool DeleteDishIngredientsOnOldDish(int id)
        {
            var result = false;
            try
            {
                var oldDishIngredients = _context.DishIngredients
                    .Where(di => di.DishId == id)
                    .ToList();
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

        public Dish CreateNewDish(CreateViewModel createDish)
        {
            var categories = _context.Categories.ToList();
            var newDish = new Dish
            {
                DishId = _context.Dishes.Last().DishId + 1,
                Category = new Category
                {
                    CategoryId = createDish.Dish.Category.CategoryId,
                    Name = categories.FirstOrDefault(c => c.CategoryId == createDish.Dish.Category.CategoryId).Name
                },
                Name = createDish.Dish.Name,
                Price = createDish.Dish.Price,
                Picture = createDish.Dish.Picture

            };
            var ingredients = _context.Ingredients.ToList();
            foreach (var dishIngredient in createDish.Ingredients)
            {
                if (dishIngredient.Selected)
                {
                    var newDishIngredient = new DishIngredient
                    {
                        Dish = newDish,
                        DishId = newDish.DishId,
                        Ingredient = ingredients.FirstOrDefault(i => i.IngredientId == int.Parse(dishIngredient.Value)),
                        IngredientId = int.Parse(dishIngredient.Value)
                    };
                    newDish.DishIngredients.Add(newDishIngredient);
                }
            }
            return newDish;
        }

        public CustomizeViewModel BuildCustomizeViewModel(int id)
        {
            var dish = _context.Dishes
               .Single(d => d.DishId == id);
            var dishIngredients = _context.DishIngredients
                .Include(di => di.Ingredient)
                .Where(di => di.DishId == id)
                .OrderBy(di => di.IngredientId)
                .ToList();
            var allIngredients = _context.Ingredients
                .OrderBy(i => i.IngredientId)
                .ToList();

            var list = new List<SelectListItem>();
            foreach (var ingredient in allIngredients)
            {
                if (dishIngredients.Exists(i => i.IngredientId == ingredient.IngredientId))
                {
                    var ingredientChecked = new SelectListItem
                    {
                        Value = ingredient.IngredientId.ToString(),
                        Selected = true,
                        Text = ingredient.Name
                    };
                    list.Add(ingredientChecked);
                }
                else
                {
                    var ingredientNotChecked = new SelectListItem
                    {
                        Value = ingredient.IngredientId.ToString(),
                        Text = ingredient.Name
                    };
                    list.Add(ingredientNotChecked);
                }
            }

            var viewModel = new CustomizeViewModel
            {
                Dish = dish,
                Ingredients = list
            };

            return viewModel;
        }

        public CreateViewModel BuildCreateViewModel()
        {
            var categories = new List<SelectListItem>();
            var ingredients = new List<SelectListItem>();
            foreach (var ingredient in _context.Ingredients.ToList())
            {
                ingredients.Add(new SelectListItem
                {
                    Text = ingredient.Name,
                    Value = ingredient.IngredientId.ToString()
                });
            }
            foreach (var category in _context.Categories.ToList())
            {
                categories.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.CategoryId.ToString()
                });
            }
            var viewModel = new CreateViewModel
            {
                Categories = categories,
                Ingredients = ingredients,
                Dish = new Dish()
            };

            return viewModel;
        }

    }
}
