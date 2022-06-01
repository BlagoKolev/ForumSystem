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
            SeedDatabase(services);

            return app;
        }

        private static void SeedDatabase(IServiceProvider services)
        {
            SeedAdministrator(services);
            SeedCategories(services);
            SeedSubCategories(services);
            SeedPosts(services);
            SeedComments(services);
            SeedAnswers(services);
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

                await userManager.CreateAsync(adminUser, adminPassword);
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
            var admin = GetAdminId(db);

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
                    Contents = "Hi there, someone help me with a supplements to weight loss?",
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
                    Contents = "What is the schema of taking Stromba?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });
                db.Posts.Add(new Post
                {
                    CreatorId = admin.Id,
                    SubCategoryId = 15,
                    Title = "Anavar",
                    Contents = "What is the schema of taking Anavar?",
                    PublishedOn = DateTime.UtcNow.ToLocalTime(),
                });

                db.SaveChanges();
            }
        }

        private static void SeedComments(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();
            var admin = GetAdminId(db);

            if (!db.Comments.Any())
            {
                db.Comments.Add(new Comment
                {
                    PostId = 1,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "No, it is stupid."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 1,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "No, it`s just a madness."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 1,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Only if u want to gain mass."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 2,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Make a pyramid."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 2,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Don`t take this shit man!"
                });

                db.Comments.Add(new Comment
                {
                    PostId = 3,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You will die young."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 3,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You will ruin your health."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 3,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You will be very big and hard but dead."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 4,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Try with slow carbohydrates."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 4,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Try with cofein and slow carbohydrates."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 4,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Forget the pills and start to eat quality food."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 5,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Try L-Carnitine and more cardio."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 5,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Fat burners + L-Carnitine + cardio."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 5,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You do not need all this staff, just eat carefully and train hard."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 6,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Depends of company that produces the product."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 6,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "2 gr per day. 30 minutes before training."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 7,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Nice food, hard trainings and good long sleep is all you need."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 7,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You need L-Carnitine and Fat burner."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 8,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You need Gainer protein"
                });
                db.Comments.Add(new Comment
                {
                    PostId = 7,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You need Steroids, amino acid and a lot ot protein."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 7,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You only need good food."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 8,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You need Steroids, protein and good food."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 9,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Stop carbohyrrates now."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 9,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Decrease carbohydrates and increase chicken protein."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 10,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "My protein."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 10,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Weider."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 10,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Animal."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 10,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Next lab."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 11,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Stop carbohydrates."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 11,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Stop midnight eating, all nights breakfast at all."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 11,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Start a hard cardio training."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 11,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Start heavy weight trainings."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 12,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "It is a hardest thing... ."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 12,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "It is very hard work.Start with decreasing carbohydrates to minimum."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 13,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Meat and vegetables, and must eat 4-5 times per day."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 13,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Meat, fish and vegetables. You must eat 5-7 times per day."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 14,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Go to doctor."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 14,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Stop trainings."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 15,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Go to orthoped."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 15,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Stop to train."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 16,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Stop to squat."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 16,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Stop training."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 17,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Eat meat and egs and train hard."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 17,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Start training your back twice per week."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 17,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Start to dead-lifting."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 18,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Start training your chest twice per week."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 18,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Do more base exercises."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 19,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Start to do squats."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 19,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Do not train other muscle group when you train your legs."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 20,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You need cardio, every day."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 20,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You need cardio, every day and L-Carnitine."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 20,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You need alternate cardio and heavy-weight trainings."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 21,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "The best way is to start a lot of cardio and decrease a quantity per meal."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 21,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Give us more info, it is hard to give you advice without more information."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 22,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Depends on age, lifestyle and much more... ."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 23,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Heavy-weight trainings."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 23,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You must stop cardio."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 24,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You are too young to gain too much mass."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 24,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Start eating a lot of protein and carbohydrates."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 25,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You must give us more info about you."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 25,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Be more specific. Tell us more about you and your trainings."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 26,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Come on, i`m your guy. Gime me some contacts."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 26,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Street fitnes is great."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 26,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Street fitnes is for ladies."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 27,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You must eat everything."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 27,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You must eat meat and vegetables."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 28,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "You must take whey protein."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 28,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Better do not eat anything."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 29,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Pyramid."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 29,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Better do not take this shit."
                });

                db.Comments.Add(new Comment
                {
                    PostId = 30,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Pyramid i think and then reverse pyramid."
                });
                db.Comments.Add(new Comment
                {
                    PostId = 29,
                    CreatorId = admin.Id,
                    PublishedOn = DateTime.UtcNow,
                    Contents = "Better do not take Anavar, it is dangerouse for your health."
                });

                db.SaveChanges();
            }
        }

        private static void SeedAnswers(IServiceProvider services)
        {
            var db = services.GetRequiredService<ApplicationDbContext>();
            var admin = GetAdminId(db);

            if (!db.Answers.Any())
            {
                for (int i = 1; i < 72; i++)
                {
                    db.Answers.Add(new Answer
                    {
                        CommentId = i,
                        PublishedOn = DateTime.UtcNow,
                        CreatorId = admin.Id,
                        CreatorName = admin.UserName,
                        Contents = "Thank you!"
                    });
                    db.Answers.Add(new Answer
                    {
                        CommentId = i,
                        PublishedOn = DateTime.UtcNow,
                        CreatorId = admin.Id,
                        CreatorName = admin.UserName,
                        Contents = "Thanks a lot! It was very usefull!"
                    });
                    db.Answers.Add(new Answer
                    {
                        CommentId = i,
                        PublishedOn = DateTime.UtcNow,
                        CreatorId = admin.Id,
                        CreatorName = admin.UserName,
                        Contents = "I agree."
                    });
                    db.Answers.Add(new Answer
                    {
                        CommentId = i,
                        PublishedOn = DateTime.UtcNow,
                        CreatorId = admin.Id,
                        CreatorName = admin.UserName,
                        Contents = "I am not sure..."
                    });
                }
                db.SaveChanges();
            }
        }

        private static IdentityUser GetAdminId(ApplicationDbContext db)
        {
            var admin = db.Users.Where(x => x.Email == "admin@fitnessforum.com").FirstOrDefault();
            return admin;
        }
    }
}
