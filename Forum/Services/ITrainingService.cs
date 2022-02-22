using System.Collections.Generic;
using Forum.Models.SubCategories;


namespace Forum.Services
{
    public interface ITrainingService
    {
        ICollection<SubCategoryAllPostsViewModel> GetAllPosts(string actionName);
    }
}
