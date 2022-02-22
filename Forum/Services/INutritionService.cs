using Forum.Models.SubCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface INutritionService
    {
        ICollection<SubCategoryAllPostsViewModel> GetAllPosts(string actionName);
    }
}
