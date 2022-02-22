using Forum.Models.SubCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface IPostService
    {
        ICollection<SubCategoryAllPostsViewModel> GetAllPosts(string actionName);
        public string GetCategoryName(string actionName);
    }
}
