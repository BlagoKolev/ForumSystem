using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            this.Answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Contents { get; set; }
        public virtual Post Post { get; set; }
        public int PostId { get; set; }
        public IdentityUser Creator { get; set; }
        public string CreatorId { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
