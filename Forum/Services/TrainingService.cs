using Forum.Data;
using Forum.Data.Models;
using Forum.Models.Trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ApplicationDbContext db;

        public TrainingService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public ICollection<AllTrainingsPostsViewModel> GetAllPosts(string actionName)
        {
            var allPosts = this.db.Posts
                 .Where(x => !x.IsDeleted)
                 .Where(x=>x.SubCategory.Name == actionName)
                 .Select(x => new AllTrainingsPostsViewModel
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Contents = x.Contents,
                     SubCategoryName = x.SubCategory.Name,
                     CategoryName = x.SubCategory.ParentCategory.Name,
                     Creator = x.Creator.Email,
                     PublishedOn = x.PublishedOn.Date,
                     Comments = x.Comments
                 })
                 .ToList();
            return allPosts;
        }
    }
}
