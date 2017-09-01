using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NackaPizzaOnline.Data;
using NackaPizzaOnline.Models;
using NackaPizzaOnline.Models.EditViewModels;
using NackaPizzaOnline.Services;

namespace NackaPizzaOnline.Controllers
{
    public class DishController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DishIngredientService _dishIngredientService;

        public DishController(ApplicationDbContext context, DishIngredientService dishIngredientService)
        {
            _context = context;
            _dishIngredientService = dishIngredientService;
        }

        // GET: Dish
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dishes.Include("Category").ToListAsync());
        }

        // GET: Dish/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dish/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
            ViewBag.Ingredients = new SelectList(_context.Ingredients, "IngredientId", "Name");
            return View();
        }

        // POST: Dish/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishId,Name,Price,Picture,CategoryId")] Dish dish)
        {
            //TODO Anpassa denna för att kunna ta in infon från select2 dropdowns. 
            if (ModelState.IsValid)
            {
                _context.Add(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dish);
        }

        // GET: Dish/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = _context.Dishes
                .Include(d => d.Category)
                .Include(d => d.DishIngredients)
                .ThenInclude(i => i.Ingredient)
                .SingleOrDefault(m => m.DishId == id);

            var allIngredients = _context.Ingredients.ToList();

            var viewModel = new EditViewModel(dish, allIngredients);

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");

            if (dish == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Dish/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditViewModel Model)
        //int id, [Bind("DishId,Name,Price,Category")] Dish dish, string[] ingredients
        {
            //TODO När det funkar att lägga till bild, lägg till i Bind. 
            //TODO Ta med int arrays ingrediensId:n och leta fram dom backend och bygg upp en ny dishIngredient.
            if (id != Model.Dish.DishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_dishIngredientService.DeleteDishIngredientsOnOldDish(id))
                    {

                       var dish = _dishIngredientService.GetEditedDish(Model);
                        
                        _context.Update(dish);
                        await _context.SaveChangesAsync();
                    }
                    //TODO Måste radera dom gamla Dishingredients som finns i dish
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(Model.Dish.DishId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Model);//dish
        }

        // GET: Dish/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.DishId == id);
        }
    }
}
