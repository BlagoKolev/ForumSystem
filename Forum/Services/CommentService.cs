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
        public Comment CreateComment(CreateCommentViewModel newCommentData, string userId)
        {
            //var currentUserWithComment = this.db.Users
            //    .Where(x => x.Id == userId)
            //    .FirstOrDefault();//to be commented

            var newComment = new Comment
            {
                Contents = newCommentData.Contents,
                CreatorId = userId,
               //Creator = currentUserWithComment, //to be commented
                PostId = newCommentData.PostId,
                PublishedOn = newCommentData.PublishedOn,
                Comments = new HashSet<Comment>(),
            };

            db.Comments.Add(newComment);
            db.SaveChanges();
            return newComment;
        }
    }
}
