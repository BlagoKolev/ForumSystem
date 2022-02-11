using System.Collections.Generic;
using Forum.Data.Models;

namespace Forum.Services
{
    public interface IHomeService
    {
        List<Category> GetCategories();
    }
}
