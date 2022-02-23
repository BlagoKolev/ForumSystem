using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models.Home
{
    public class HomeCategoriesViewModel
    {
        public HomeCategoriesViewModel()
        {
            this.SubCategories = new HashSet<SubCategory>();
        }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }

    }
}
