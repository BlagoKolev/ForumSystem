using Forum.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models.Post
{
    public class CreatePostViewModel
    {
        public CreatePostViewModel()
        {
            this.Comments = new HashSet<Comment>();
        }
        [Key]
        public int Id { get; set; }

        public Category Category { get; set; }

        [Display(Name = "Main-Category")]
        public int CategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        [Display(Name = "Sub-Category")]
        public int SubCategoryId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Title must be between 10 and 200 symbols.")]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 50)]
        public string Contents { get; set; }
        public virtual IdentityUser Creator { get; set; }
        public string CreatorId { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }


    }
}
