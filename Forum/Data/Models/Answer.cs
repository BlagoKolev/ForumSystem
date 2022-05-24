using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Data.Models
{
    public class Answer
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Contents { get; set; }
        public IdentityUser Creator { get; set; }
        public string CreatorName { get; set; } //new Prop experimental
        public string CreatorId { get; set; }
        public DateTime PublishedOn { get; set; }
        public Comment Comment { get; set; }
        public int CommentId { get; set; }
        public bool IsDeleted { get; set; }
        // public bool IsAnswer { get; set; }
        //public virtual ICollection<Answer> Answers { get; set; }
    }
}
