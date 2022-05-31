using static Forum.Data.GlobalConstants;
using Forum.Models.Post;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

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
        [Authorize]
        public IActionResult CreateComment(ReadPostViewModel comment)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction();
            }

            var userId = GetUserId();
            var newCommentData = comment.CreateCommentViewModel;
            var newPost = commentService.CreateComment(newCommentData, userId);
            if (newPost == null)
            {
                TempData[AlertMessageFailKey] = AlertMessageFail.ActionFailed;
                return RedirectToAction();
            }

            return this.Redirect($"/Post/Discussion?postId={newCommentData.PostId}");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateAnswer(ReadPostViewModel answer, int Id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction();
            }

            var userId = GetUserId();
            var newAnswerData = answer.CreateCommentViewModel;
            var newAnswer = commentService.CreateAnswer(newAnswerData, userId, Id);
            return Redirect($"/Post/Discussion?postid={newAnswerData.PostId}");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int commentId)
        {
            int postId = await commentService.DeleteComment(commentId);

            if (postId == 0)
            {
                TempData[AlertMessageFailKey] = AlertMessageFail.CannotDeleteComment;
                return Redirect("/");
            }
            TempData[AlertMessageKey] = AlertMessage.CommentDeleted;
            return Redirect($"/Post/Discussion?postId={postId}");
        }
        private string GetUserId()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
