using System;
using System.Collections.Generic;
using Forum.Data.Models;

namespace Forum.Models.Post
{
    public class ReadPostViewModel
    {
        public ReadPostViewModel()
        {
            this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string Creator { get; set; }
        public string CreatorId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public DateTime PublishedOn { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
