using System.Collections.Generic;
using Forum.Data.Models;
using Forum.Models.Home;

namespace Forum.Services
{
    public interface IHomeService
    {
        List<HomeCategoriesViewModel> GetCategories();
    }
}
