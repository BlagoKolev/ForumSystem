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
            SeedPosts(services);

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

        private static void SeedPosts(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();
            var admin = db.Users.Where(x => x.Email == "admin@fitnessforum.com").FirstOrDefault();

            if (!db.Posts.Any())
            {
                //Posts for Trainings - Injuries.
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 1,
                    Title = "Hard pain in the back.",
                    Contents = "Hello there, i`m new in fitness and i have a pain in my back. Do you have in mind what is the reason?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 1,
                    Title = "Hard pain in the neck.",
                    Contents = "Hello guys. I train with heavy weights and there is constant pain in my neck. Someone to help me?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 1,
                    Title = "Pain in the knees.",
                    Contents = "Hello guys. When I squat for a few days my knees hurt. What could be the reason?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Posts for Trainings - Exercises

                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 2,
                    Title = "I want to add some mass in my back.",
                    Contents = "Hello guys. I want to add some mass in my back. Suggest me some exercises please. ",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 2,
                    Title = "Big chest",
                    Contents = "Hello guys. I want to add some mass in my chest. Suggest me some program to follow please.",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 2,
                    Title = "Weak legs.",
                    Contents = "Hi there. I want to build a massive legs. May someone told me a good program to follow?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Posts for Trainings - Weightloss trainings

                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 3,
                    Title = "I am fat",
                    Contents = "Hi there. I am overweight and i want to lose about 20 kilos. Suggest me please, a proper program to follow.",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 3,
                    Title = "I am too fat",
                    Contents = "Hi there. I want to lose 5 to 10 kilos for month. What is the best way?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 3,
                    Title = "Fats problem",
                    Contents = "Hello, i have a problem with fats on my belly. What is the right way to lose these fats?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Posts for Trainings - Muscle mass increase training program

                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 4,
                    Title = "I am too skinny",
                    Contents = "Hi there. I want to gain 10 to 15 kilos. What is the best training program for me?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 4,
                    Title = "I want to gain mass",
                    Contents = "Hi there. I`m 16 years old and i have been training for 2 months. What is the fastest way to gain mass?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 4,
                    Title = "Gain mass problem.",
                    Contents = "Hi there. I want to gain muscle mass and weight, but whatever I do doesn't work. Anybody help me?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Post for Trainings - Street fitness
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 5,
                    Title = "I`m new.",
                    Contents = "Hi guys. I want to start practicing Street fitness, but i know nothing about it. Someone to train with me?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Posts for Nutrition - Increase muscle mass
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 7,
                    Title = "I`m new.",
                    Contents = "Hello, everyone. I want to increase strength and muscle mass. What must i eat?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 7,
                    Title = "Problem",
                    Contents = "Hi, everyone. What must i eat late at night to increase clean muscle mass?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 7,
                    Title = "Midnight, breakfast.",
                    Contents = "Hi, builders. Is it correct to eat carbohydrates, before sleep?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Posts for Nutrition - Weight loss
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 8,
                    Title = "I want to be fit.",
                    Contents = "Hello, guys. What must be the all meals for the day, and how many times i must eat per day, to lose some kilos?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 8,
                    Title = "I want clean up my belly.",
                    Contents = "Hello, guys. Share with me a diet to lose bellys fat.",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 8,
                    Title = "I want to lose 10 kilos.",
                    Contents = "Hi, guys. What diet should I follow to lose 10 kilos for month?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Posts for Supplements - Increase mass and Strength
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 10,
                    Title = "About Protein",
                    Contents = "Hi, there. Tell me a good protein for gainning mass.",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 10,
                    Title = "I want to gain mass",
                    Contents = "Hello, guys. Tell me a good way to increase clean muscle mass.",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 10,
                    Title = "Best for increase muscle mass.",
                    Contents = "Hello, guys. I have read a lot about supplements that can help me to increase a clean muscle mass, but i`m confused ot so many info. May someone to help me with advice?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Posts for Supplements - Weight loss

                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 11,
                    Title = "Best way to fat loss",
                    Contents = "Hello, guys. I have read a lot about supplements that can help me to lose some fats, but i`m confused ot so many info. May someone to help me with advice?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 11,
                    Title = "L-Carnitine",
                    Contents = "Hello, guys. May someone give me advice, how to take L-Carnitine?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 11,
                    Title = "I want to get in shape.",
                    Contents = "Hi there, someone help me with a supplements to waight loss?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Posts for Supplements - Energy
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 12,
                    Title = "Looking for supplements that give energy.",
                    Contents = "I train a lot, but feel a lack of energy too often. Someone to share a experience with energy supplements?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Posts for Steroids - Increase mass
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 14,
                    Title = "Steroids opinion",
                    Contents = "Is there someone to tell me side effects of taking a steroids?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 14,
                    Title = "Bionabol",
                    Contents = "What is the schema of takia Bionabol?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                //Posts for Steroids - Weight loss
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 15,
                    Title = "Stromba",
                    Contents = "What is the schema of takia Stromba?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 15,
                    Title = "Anavar",
                    Contents = "What is the schema of takia Anavar?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                db.SaveChanges();
            }
        }
    }
}
