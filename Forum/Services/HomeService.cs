using Forum.Data;
using Forum.Data.Models;
using Forum.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext db;

        public HomeService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public List<HomeCategoriesViewModel> GetCategories()
        {
            var categories = this.db.Categories
                .Where(c=>!c.IsDeleted)
                .Select(c => new HomeCategoriesViewModel
                {
                    CategoryName = c.Name,
                    CategoryId = c.Id,
                    SubCategories = c.SubCategories
                   .OrderBy(c => c.Name.Length)
                   .ToList()
                })
                .ToList();


            return categories;
        }
    }
}
