using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Helper
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedCategories(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();
            if (!db.Categories.Any())
            {
                db.Categories.Add(new Category { Name = "Trainings" });
                db.Categories.Add(new Category { Name = "Nutrition" });
                db.Categories.Add(new Category { Name = "Nutritional Supplements" });
                db.Categories.Add(new Category { Name = "Anabolic Steroids and Prohibited Substances" });

                db.SaveChanges();
            }
        }
    }
}
