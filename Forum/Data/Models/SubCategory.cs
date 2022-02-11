using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Data.Models
{
    public class SubCategory
    {
        public SubCategory()
        {
            this.Posts = new HashSet<Post>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
