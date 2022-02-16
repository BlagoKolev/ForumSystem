using Forum.Data.Models;
using Forum.Models.SubCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface ITrainingService
    {
        ICollection<SubCategoryAllPostsViewModel> GetAllPosts(string actionName);
    }
}
