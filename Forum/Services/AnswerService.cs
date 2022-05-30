using Forum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly ApplicationDbContext db;

        public AnswerService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<int> DeleteAnswer(int answerId)
        {
            var answer = db.Answers
                 .Where(x => x.Id == answerId)
                 .FirstOrDefault();

            var postId = db.Posts
                .Where(x => x.Comments.Any(x => x.Answers.Any(a => a.Id == answerId)))
                .Select(x => x.Id)
                .FirstOrDefault();

            if (answer != null)
            {
                answer.IsDeleted = true;
                await db.SaveChangesAsync();
            }

            return postId;
        }
    }
}
