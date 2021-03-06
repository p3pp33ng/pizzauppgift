﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NackaPizzaOnline.Models;

namespace NackaPizzaOnline.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DishIngredient>().HasKey(di => new { di.DishId, di.IngredientId });
            builder.Entity<DishIngredient>().HasOne(di => di.Dish).WithMany(di => di.DishIngredients)
                .HasForeignKey(di => di.DishId);
            builder.Entity<DishIngredient>().HasOne(di => di.Ingredient).WithMany(di => di.DishIngredients)
                .HasForeignKey(di => di.IngredientId);

            builder.Entity<CartItemIngredients>().HasKey(ci => new { ci.CartItemId, ci.IngredientId });
            builder.Entity<CartItemIngredients>().HasOne(ci => ci.CartItem).WithMany(ci => ci.CartItemIngredients)
                .HasForeignKey(ci => ci.CartItemId);
            builder.Entity<CartItemIngredients>().HasOne(di => di.Ingredient).WithMany(di => di.CartItemIngredients)
                .HasForeignKey(di => di.IngredientId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartItemIngredients> CartItemIngredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
