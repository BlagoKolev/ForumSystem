using Forum.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Answer>()               
            //    .HasOne(x => x.Comment)
            //    .WithMany(x => x.Answers)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Comment>()
            //    .HasOne(x => x.Post)
            //    .WithMany(x => x.Comments)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
