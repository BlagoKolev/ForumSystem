using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Models
{
    public class ProfilePicture
    {
        [Key]
        public int Id { get; set; }

        public Byte[] Image { get; set; }

      
        public IdentityUser Creator { get; set; }

        public string CreatorId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
