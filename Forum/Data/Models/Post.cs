using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Models
{
    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Contents { get; set; }

        public virtual IdentityUser Creator { get; set; }
        public string CreatorId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

