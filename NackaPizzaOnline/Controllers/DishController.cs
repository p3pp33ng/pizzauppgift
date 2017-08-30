using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NackaPizzaOnline.Data;
using NackaPizzaOnline.Models;

namespace NackaPizzaOnline.Controllers
{
    public class DishController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DishController(ApplicationDbContext context)
        {
            _context = context;
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

            var dish = await _context.Dishes.Include(d=>d.Category).Include(d=>d.DishIngredients).SingleOrDefaultAsync(m => m.DishId == id);
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
            //TODO Måste se till så att dom ingrdienser som redan finns på matrtten ska dyka upp i select2.
            ViewBag.Ingredients = new MultiSelectList(_context.Ingredients, "IngredientId", "Name");
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }

        // POST: Dish/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishId,Name,Price,Category,DishIngredients")] Dish dish, List<Ingredient> Ingredients)
        {           
            //TODO När det funkar att lägga till bild, lägg till i Bind. 
            if (id != dish.DishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishId))
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
            return View(dish);
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
