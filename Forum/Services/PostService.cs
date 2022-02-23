using Forum.Data;
using Forum.Data.Models;
using Forum.Models.Post;
using Forum.Models.SubCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext db;

        public PostService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreatePost(CreatePostViewModel createPostModel, string userId)
        {
            var newPost = new Post
            {
                Title = createPostModel.Title,
                Contents = createPostModel.Contents,
                CreatorId = userId,
                PublishedOn = DateTime.UtcNow,
                SubCategoryId = createPostModel.SubCategoryId,
                Comments = new HashSet<Comment>()
            };

            db.Posts.Add(newPost);
            db.SaveChanges();
        }

        public ICollection<SubCategoryAllPostsViewModel> GetAllPosts(string actionName)
        {
            var allPosts = this.db.Posts
               .Where(x => !x.IsDeleted)
               .Where(x => x.SubCategory.Name == actionName)
               .Select(x => new SubCategoryAllPostsViewModel
               {
                   Id = x.Id,
                   Title = x.Title,
                   Contents = x.Contents,
                   SubCategoryName = x.SubCategory.Name,
                   CategoryName = x.SubCategory.ParentCategory.Name,
                   Creator = x.Creator.Email,
                   PublishedOn = x.PublishedOn.ToLocalTime(),
                   Comments = x.Comments
               })
               .ToList();
            return allPosts;
        }

        public string GetCategoryName(string actionName)
        {
            var categoryName = actionName.Replace('-', ' ');
            return categoryName;
        }

        public ReadPostViewModel GetPostById(int postId)
        {
            var searchedPost = db.Posts
                .Where(x => x.Id == postId && !x.IsDeleted)
                .Select(x => new ReadPostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Contents = x.Contents,
                    CategoryName = x.SubCategory.ParentCategory.Name,
                    CategoryId = x.SubCategory.ParentCategoryId,
                    SubCategoryName = x.SubCategory.Name,
                    SubCategoryId = x.SubCategoryId,
                    Creator = x.Creator.UserName,
                    CreatorId = x.CreatorId,
                    PublishedOn = x.PublishedOn,
                    Comments = x.Comments
                })
                .FirstOrDefault();

            return searchedPost;    
        }
    }
}
