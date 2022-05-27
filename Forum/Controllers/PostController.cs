using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Forum.Models.Post;
using Forum.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Forum.Models.SubCategories;
using Microsoft.AspNetCore.Identity;
using Forum.Models.Comments;
using Forum.Data;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly IHomeService homeService;
        private readonly IPostService postService;
        private readonly UserManager<IdentityUser> userManager;

        public PostController(IHomeService homeService,
            IPostService postService,
            UserManager<IdentityUser> userManager)
        {
            this.homeService = homeService;
            this.postService = postService;
            this.userManager = userManager;
        }

        public IActionResult Discussion(int postId)
        {
            //var user = await this.userManager.GetUserAsync(HttpContext.User);
            var searchedPost = postService.GetPostById(postId);
            if (searchedPost == null)
            {
                Response.StatusCode = 404;
                return Redirect("/Errors/ResourceNotFound");
               // return View("Error"); TODO
               
            }
            //searchedPost.CreateCommentViewModel = new CreateCommentViewModel();
            //searchedPost.CreateCommentViewModel.Creator = user;
            //foreach (var comment in searchedPost.Comments)
            //{
            //    comment.Creator = await userManager.FindByIdAsync($"{comment.CreatorId}");
            //}
            return this.View(searchedPost);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.CategoryId = homeService
                .GetCategories()
                .Select(c => new SelectListItem
                { Value = c.CategoryId.ToString(), Text = c.CategoryName })
                .ToList();


            return View();
        }

        public IActionResult GetSubCategory(int mainCategoryId)
        {
            var SubCategoryList = homeService.GetCategories()
                .Where(x => x.CategoryId == mainCategoryId)
                .FirstOrDefault()
                .SubCategories
                .Select(c => new { Id = c.Id, Name = c.Name }).ToList();
            return Json(SubCategoryList);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreatePostViewModel createPostModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction();
            }

            var userId = GetUserId();
            postService.CreatePost(createPostModel, userId);
            

            return this.Redirect("/Home/Index");
        }

        public async Task<IActionResult> Delete(int postId)
        {
            var isDeleted = await postService.DeletePost(postId);

            if (isDeleted)
            {
                TempData[GlobalConstants.AlertMessageKey] = GlobalConstants.AlertMessage.PostDeleted;
            }

            return Redirect("/Home/Index");
        }

        private string GetUserId()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
