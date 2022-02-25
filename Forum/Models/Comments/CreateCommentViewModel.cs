using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Forum.Models.Comments
{
    public class CreateCommentViewModel
    {
        public int SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 50)]
        public string Contents { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Title must be between 10 and 200 symbols.")]
        public string Title { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool IsDeleted { get; set; }
       // public virtual IdentityUser Creator { get; set; }
        public string CreatorId { get; set; }

        public int PostId { get; set; }
    }
}
