using Forum.Models.Comments;
using Forum.Models.Post;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }


        [HttpPost]
        public IActionResult Create(ReadPostViewModel comment)
        {
            var userId = GetUserId();
            var newCommentData = comment.CreateCommentViewModel;
            var newPost = commentService.CreateComment(newCommentData, userId);
            if (newPost == null)
            {
                //TODO
            }

            return this.Redirect($"/Post/Discussion?postId={newCommentData.PostId}");
        }
        private string GetUserId()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
