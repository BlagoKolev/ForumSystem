using Forum.Data;
using Forum.Data.Models;
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
        public List<Category> GetCategories()
        {
            return this.db.Categories.ToList();
        }
    }
}
