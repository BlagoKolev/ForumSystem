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

        public Answer CreateAnswer(CreateCommentViewModel newAnswerData, string userId, int commentId)
        {
            var creator = this.db.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var newAnswer = new Answer
            {
                Contents = newAnswerData.Contents,
                CreatorName = creator.UserName, //experimental prop
                CreatorId = userId,
                Creator = creator,
                //PostId = newAnswerData.PostId,
                PublishedOn = newAnswerData.PublishedOn,
                CommentId = commentId
            };

            //var comment = this.db.Comments
            //    .Where(x => x.Id == commentId && !x.IsDeleted)
            //    .Select(x=>x.Answers)
            //    .FirstOrDefault();

            //comment.Answers.Add(newAnswer);
            this.db.Answers.Add(newAnswer);
            this.db.SaveChanges();

            return newAnswer;
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
                PostId = newCommentData.PostId,
                PublishedOn = newCommentData.PublishedOn,
            };

            db.Comments.Add(newComment);
            db.SaveChanges();
            return newComment;
        }

        public async Task<int> DeleteComment(int commentId)
        {
            var answers = db.Answers
            .Where(x => x.CommentId == commentId)
            .ToList();

            if (answers.Any())
            {
                foreach (var answer in answers)
                {
                    answer.IsDeleted = true;
                }
            }

            var comment = db.Comments
                .Where(x => x.Id == commentId)
                .FirstOrDefault();

            if (comment != null)
            {
                comment.IsDeleted = true;
            }

            var postId = db.Comments
                .Where(x => x.Id == commentId)
                .Select(x => x.PostId)
                .FirstOrDefault();

            await db.SaveChangesAsync();

            return postId;
        }
    }
}
