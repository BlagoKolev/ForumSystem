using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public Category ParentCategory { get; set; }
        public int ParentCategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
