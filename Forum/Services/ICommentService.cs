using Forum.Data.Models;
using Forum.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface ICommentService
    {
        public void CreateComment(CreateCommentViewModel newCommentData, string userId);
    }
}
