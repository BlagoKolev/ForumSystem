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
        public Comment CreateComment(CreateCommentViewModel newCommentData, string userId);
        public Answer CreateAnswer(CreateCommentViewModel newAnswerData, string userId, int postId);
    }
}
