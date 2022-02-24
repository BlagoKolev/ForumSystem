using Forum.Data;
using Forum.Data.Models;
using Forum.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext db;

        public CommentService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateComment(CreateCommentViewModel newCommentData, string userId)
        {
            var newComment = new Comment
            {
                Contents = newCommentData.Contents,
                CreatorId = userId,
                PostId = newCommentData.Id,
                PublishedOn = newCommentData.PublishedOn,
                Comments = new HashSet<Comment>(),
            };

            db.Comments.Add(newComment);
            db.SaveChanges();
        }
    }
}
