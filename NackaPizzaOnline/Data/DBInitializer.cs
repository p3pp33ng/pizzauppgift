﻿using NackaPizzaOnline.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace NackaPizzaOnline.Data
{
    public static class DBInitializer
    {
        public static void Initialize(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var aUser = new ApplicationUser
            {
                UserName = "user@mail.com",
                Email = "user@mail.com"
            };
            IdentityResult result = userManager.CreateAsync(aUser, "pa$$w0rd").Result;

            var adminRole = new IdentityRole { Name = "Admin" };
            IdentityResult roleResult = roleManager.CreateAsync(adminRole).Result;

            var adminUser = new ApplicationUser
            {
                UserName = "admin@mail.com",
                Email = "admin@mail.com"
            };
            IdentityResult adminResult = userManager.CreateAsync(adminUser, "Adm1n$").Result;

            userManager.AddToRoleAsync(adminUser, adminRole.Name);


            if (!context.Dishes.Any())
            {
                var chesse = new Ingredient { Name = "Ost", PriceIfExtra = 5 };
                var tomato = new Ingredient { Name = "Tomat", PriceIfExtra = 5 };
                var ham = new Ingredient { Name = "Skinka", PriceIfExtra = 5 };
                var pineapple = new Ingredient { Name = "Annanas", PriceIfExtra = 5 };
                var chicken = new Ingredient { Name = "Kyckling", PriceIfExtra = 10 };
                var curry = new Ingredient { Name = "Curry", PriceIfExtra = 10 };
                var pepper = new Ingredient { Name = "Paprika", PriceIfExtra = 5 };

                var pizza = new Category { Name = "Pizza" };
                var sallad = new Category { Name = "Sallad" };
                var thai = new Category { Name = "Thailändsk kök" };
                var pasta = new Category { Name = "Pasta" };

                var margarita = new Dish { Name = "Margarita", CategoryId = 1, Price = 60 };
                var hawaii = new Dish { Name = "Hawaii", CategoryId = 1, Price = 65 };
                var chickenRedCurry = new Dish { Name = "Kyckling röd curry", CategoryId = 3, Price = 70 };

                var thaiChicken = new DishIngredient { Dish = chickenRedCurry, Ingredient = chicken };
                var thaiCurry = new DishIngredient { Dish = chickenRedCurry, Ingredient = curry };
                var thaiPepper = new DishIngredient { Dish = chickenRedCurry, Ingredient = pepper };
                var marChesse = new DishIngredient { Dish = margarita, Ingredient = chesse };
                var martomato = new DishIngredient { Dish = margarita, Ingredient = tomato };
                var marHam = new DishIngredient { Dish = margarita, Ingredient = ham };
                var hawChesse = new DishIngredient { Dish = hawaii, Ingredient = chesse };
                var hawTomato = new DishIngredient { Dish = hawaii, Ingredient = tomato };
                var hawHam = new DishIngredient { Dish = hawaii, Ingredient = ham };
                var hawPineapple = new DishIngredient { Dish = hawaii, Ingredient = pineapple };

                margarita.DishIngredients.Add(marChesse);
                margarita.DishIngredients.Add(martomato);
                margarita.DishIngredients.Add(marHam);

                hawaii.DishIngredients.Add(hawChesse);
                hawaii.DishIngredients.Add(hawHam);
                hawaii.DishIngredients.Add(hawPineapple);
                hawaii.DishIngredients.Add(hawTomato);

                chickenRedCurry.DishIngredients.Add(thaiChicken);
                chickenRedCurry.DishIngredients.Add(thaiCurry);
                chickenRedCurry.DishIngredients.Add(thaiPepper);

                context.Ingredients.AddRange(
                    chesse,
                    tomato,
                    ham,
                    pineapple,
                    chicken,
                    curry,
                    pepper);
                context.Categories.AddRange(
                    pizza,
                    sallad,
                    pasta,
                    thai);
                context.Dishes.AddRange(
                    margarita,
                    hawaii,
                    chickenRedCurry);
                context.DishIngredients.AddRange(
                    thaiChicken,
                    thaiCurry,
                    thaiPepper,
                    marChesse,
                    marHam,
                    martomato,
                    hawChesse,
                    hawHam,
                    hawPineapple,
                    hawTomato);
                context.SaveChanges();
            }
        }
    }
}