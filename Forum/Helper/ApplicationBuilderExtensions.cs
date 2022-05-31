using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
            SeedAdministrator(services);
            SeedCategories(services);
            SeedSubCategories(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync("Admin"))
                {
                    return;
                }

                var adminRole = new IdentityRole { Name = "Admin" };

                await roleManager.CreateAsync(adminRole);

                var adminEmail = "admin@fitnessforum.com";
                var adminPassword = "admin";

                var adminUser = new IdentityUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                };

                await userManager.CreateAsync(adminUser,adminPassword);
                await userManager.AddToRoleAsync(adminUser, adminRole.Name);
            })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();

            if (!db.Categories.Any())
            {
                db.Categories.Add(new Category { Name = "Trainings" });
                db.Categories.Add(new Category { Name = "Nutrition" });
                db.Categories.Add(new Category { Name = "Supplements" });
                db.Categories.Add(new Category { Name = "Steroids" });

                db.SaveChanges();
            }
        }

        private static void SeedSubCategories(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();

            if (!db.SubCategories.Any())
            {

                //Trainings
                db.SubCategories.Add(new SubCategory
                {
                    Name = "Мuscle mass increase training programs",
                    ParentCategoryId = 1
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Weight loss training programs",
                    ParentCategoryId = 1
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Exercises",
                    ParentCategoryId = 1
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Injuries and health problems",
                    ParentCategoryId = 1
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Street fitness",
                    ParentCategoryId = 1
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Common discussions",
                    ParentCategoryId = 1
                });

                //Nutrition
                db.SubCategories.Add(new SubCategory
                {
                    Name = "Increase muscle mass",
                    ParentCategoryId = 2
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Weight loss",
                    ParentCategoryId = 2
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Common discussions",
                    ParentCategoryId = 2
                });

                //Nutritional supplements
                db.SubCategories.Add(new SubCategory
                {
                    Name = "Increase muscle mass and strength",
                    ParentCategoryId = 3
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Weight loss and fat reduction",
                    ParentCategoryId = 3
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Energy",
                    ParentCategoryId = 3
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Common discussions",
                    ParentCategoryId = 3
                });

                //Steroids
                db.SubCategories.Add(new SubCategory
                {
                    Name = "Increase muscle mass",
                    ParentCategoryId = 4
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Weight loss",
                    ParentCategoryId = 4
                });

                db.SubCategories.Add(new SubCategory
                {
                    Name = "Common discussions",
                    ParentCategoryId = 4
                });


                db.SaveChanges();
            }
        }
    }
}
