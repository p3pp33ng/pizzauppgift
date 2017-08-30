using NackaPizzaOnline.Models;
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
            var userRole = new IdentityRole { Name = "User" };
            IdentityResult userRoleResult = roleManager.CreateAsync(userRole).Result;
            var aUser = new ApplicationUser
            {
                UserName = "user@mail.com",
                Email = "user@mail.com",
                FirstName = "User",
                LastName = "Usersson",
                Address = "Usergatan 12",
                ZipCode = "12345",
                City = "Userton",
                PhoneNumber = "07000000000"
            };
            IdentityResult result = userManager.CreateAsync(aUser, "Pa$$w0rd").Result;
            userManager.AddToRoleAsync(aUser, userRole.Name);

            var adminRole = new IdentityRole { Name = "Admin" };
            IdentityResult roleResult = roleManager.CreateAsync(adminRole).Result;
            var adminUser = new ApplicationUser
            {
                UserName = "admin@mail.com",
                Email = "admin@mail.com",
                FirstName = "Admin",
                LastName = "Adminsson",
                Address = "Admingatan 12",
                ZipCode = "12345",
                City = "Adminton",
                PhoneNumber = "070562562562"
            };
            IdentityResult adminResult = userManager.CreateAsync(adminUser, "Adm1n$").Result;
            userManager.AddToRoleAsync(adminUser, adminRole.Name);


            if (!context.Dishes.Any())
            {
                var chesse = new Ingredient { Name = "Ost", PriceIfExtra = 5, IsChecked= false };
                var tomato = new Ingredient { Name = "Tomat", PriceIfExtra = 5, IsChecked = false };
                var ham = new Ingredient { Name = "Skinka", PriceIfExtra = 5, IsChecked = false };
                var pineapple = new Ingredient { Name = "Annanas", PriceIfExtra = 5, IsChecked = false };
                var chicken = new Ingredient { Name = "Kyckling", PriceIfExtra = 10, IsChecked = false };
                var curry = new Ingredient { Name = "Curry", PriceIfExtra = 10 , IsChecked = false };
                var pepper = new Ingredient { Name = "Paprika", PriceIfExtra = 5 , IsChecked = false };

                var pizza = new Category { Name = "Pizza" };
                var sallad = new Category { Name = "Sallad" };
                var thai = new Category { Name = "Thailändsk kök" };
                var pasta = new Category { Name = "Pasta" };

                var margarita = new Dish { Name = "Margarita", Category = pizza, Price = 60 };
                var hawaii = new Dish { Name = "Hawaii", Category = pizza, Price = 65 };
                var chickenRedCurry = new Dish { Name = "Kyckling röd curry", Category = thai, Price = 70 };

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
