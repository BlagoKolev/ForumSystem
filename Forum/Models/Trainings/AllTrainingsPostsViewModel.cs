using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models.Trainings
{
    public class AllTrainingsPostsViewModel
    {
        public AllTrainingsPostsViewModel()
        {
            this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string Creator { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime PublishedOn { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
